using TeamHunter.Models;
using TeamHunter.Models.DTO;
using System.Linq.Expressions;
using TeamHunter.Interfaces;
using MongoDB.Driver;
using System.Reflection;

namespace TeamHunter.Models;

class EventService : IEventService {
    private readonly IMongoCollection<Event> eventManager;
    private readonly IMongoCollection<Discussion> discussionManager;
    private readonly ISettingsService settingsService;

    public EventService(IDBSessionManagerService manager, ISettingsService settingsService){
        this.eventManager = manager.GetCollection<Event>();
        this.discussionManager = manager.GetCollection<Discussion>();
        this.settingsService = settingsService;
    }

    public async Task<List<Event>> GetEventsAsync() =>
        await this.GetEventsAsync(e => true);
    public async Task<List<Event>> GetEventsAsync(Expression<Func<Event, bool>> filter) =>
        await (await this.eventManager.FindAsync(filter)).ToListAsync();
    public async Task<Event> GetEventByIdAsync(string eventId) =>
        (await this.GetEventsAsync(e => e.Id == eventId)).First();
    public async Task<List<Event>> GetEventsByTypeAsync(string type) =>
        await this.GetEventsAsync(e => e.Type == type);
    public async Task<Event> CreateEventAsync(User owner, EventCreate eventCreate) {
        Event eventModel = eventCreate.toEvent();
        eventModel.Owner = owner.Id;

        await this.eventManager.InsertOneAsync(eventModel);
        return eventModel;
    }
    public async Task DeleteEventAsync(string eventId) =>
        await this.eventManager.DeleteOneAsync(e => e.Id == eventId);
    public async Task<Event> ModifyEventAsync(string eventId, EventUpdate eventUpdate) {
        UpdateDefinitionBuilder<Event> modification = Builders<Event>.Update;
        List<UpdateDefinition<Event>> modifications = new List<UpdateDefinition<Event>>();
        Event eventModel = await this.GetEventByIdAsync(eventId);
        PropertyInfo[] eventUpdateProperties = this.eventManager.GetType().GetProperties();
        PropertyInfo[] eventProperties = eventModel.GetType().GetProperties();

        foreach(var item in eventUpdate.GetType().GetProperties()){
            if (item.GetValue(eventUpdate) is not null) {
                modifications.Add(modification.AddToSet(item.Name, item.GetValue(eventUpdate)));

                eventProperties.First(prop => prop.Name == item.Name)
                    .SetValue(eventModel, item.GetValue(eventUpdate));
            }
        }
    
        await this.eventManager.FindOneAndUpdateAsync(u => u.Id == eventId, modification.Combine(modifications));
        return eventModel;
    }
    public async Task<bool> JoinEventAsync(string eventId, User participant) {
        Event eventModel = await this.GetEventByIdAsync(eventId);
        if (!eventModel.Participants.Contains(participant)) {
            eventModel.Participants.Add(participant);
            UpdateDefinition<Event> update = Builders<Event>.Update.Set("Participants", eventModel.Participants);

            await this.eventManager.UpdateOneAsync(e => e.Id == eventId, update);
            return true;
        }
        return false;
    }
    public async Task<bool> LeaveEventAsync(string eventId, User participant) {
        Event eventModel = await this.GetEventByIdAsync(eventId);
        if (eventModel.Participants.Contains(participant)) {
            eventModel.Participants.RemoveAt(eventModel.Participants.FindIndex(u => u.Id == participant.Id));
            UpdateDefinition<Event> update = Builders<Event>.Update.Set("Participants", eventModel.Participants);

            await this.eventManager.UpdateOneAsync(e => e.Id == eventId, update);
            return true;
        }
        return false;
    }
    public async Task<Discussion> LoadCommentsAsync(string eventId) =>
        (await this.discussionManager.FindAsync(d => d.EventId == eventId)).First();
    public async Task<Comment> PostCommentAsync(string eventId, User participant, string text, DateTime? replyToCommentId) {
        Discussion discussion = await this.LoadCommentsAsync(eventId);
        Comment newComment = new Comment() {
            Sender = participant,
            Text = text,
            ReplyTo = replyToCommentId
        };
        if (replyToCommentId is not null)
            discussion.Comments.Find(c => c.Id == replyToCommentId)!.Replies.Add(newComment.Id);

        discussion.Comments.Add(newComment);
        UpdateDefinition<Discussion> update = Builders<Discussion>.Update.Set("Messages", discussion.Comments);
        await this.discussionManager.UpdateOneAsync(d => d.EventId == eventId, update);
        await this.SyncEventWithDiscussionAsync(discussion);
        return newComment;
    }
    public async Task DeleteCommentAsync(string eventId, DateTime commentId) {
        Discussion discussion = await this.LoadCommentsAsync(eventId);
        int commentIndex = discussion.Comments.FindIndex(c => c.Id == commentId);
        List<Comment> commentsToDelete = new List<Comment>();

        if(discussion.Comments[commentIndex].ReplyTo is not null)
            discussion.Comments.Find(c => c.Id == commentId);

        Queue<Comment> commentFetcher = new Queue<Comment>();
        commentFetcher.Enqueue(discussion.Comments[commentIndex]);
        while(commentFetcher.Count() != 0){
            foreach(var comment in commentFetcher)
                commentFetcher.Enqueue(comment);
            commentsToDelete.Add(commentFetcher.Dequeue());
        }
        FilterDefinition<Discussion> filter = Builders<Discussion>.Filter.AnyEq("Comments", commentsToDelete);
        await this.discussionManager.DeleteManyAsync(filter);
    }

    public async Task SyncEventWithDiscussionAsync(Discussion discussion) {
        Event eventModel = await this.GetEventByIdAsync(discussion.EventId!);

        if (eventModel.Discussion != discussion.Comments.Take((int)this.settingsService.MessageRepliesLimit)) {
            eventModel.Discussion = discussion.Comments.Take((int)this.settingsService.MessageRepliesLimit).ToList();
            UpdateDefinition<Event> update = Builders<Event>.Update.Set("Discussion", eventModel.Discussion);
            await this.eventManager.UpdateOneAsync(e => e.Id == discussion.EventId, update);
        }
    }
}