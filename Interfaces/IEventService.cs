using TeamHunter.Models;
using TeamHunter.Models.DTO;
using System.Linq.Expressions;

namespace TeamHunter.Interfaces;

public interface IEventService
{
    Task<List<Event>> GetEventsAsync();
    Task<List<Event>> GetEventsAsync(Expression<Func<Event, bool>> filter);
    Task<Event> GetEventByIdAsync(string eventId);
    Task<List<Event>> GetEventsByTypeAsync(string type);
    Task<Event> CreateEventAsync(EventCreate eventCreate);
    Task DeleteEventAsync(string eventId);
    Task<Event> ModifyEventAsync(string eventId, EventUpdate eventUpdate);
    Task<bool> JoinEventAsync(string eventId, User participant);
    Task<bool> LeaveEventAsync(string eventId, User participant);
    Task<Discussion> LoadCommentsAsync(string eventId);
    Task<Comment> PostCommentAsync(string eventId, User participant, string text, DateTime? replyToCommentDate);
    Task DeleteCommentAsync(string eventId, DateTime commentId);
    
}