using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TeamHunter.DB;
using TeamHunter.Schemas;
using System.Linq.Expressions;

namespace TeamHunter.Services
{
    public class EventService
    {
        private readonly IMongoCollection<Event> _events;

        public EventService(IOptions<DBSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _events = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<Event>(options.Value.CollectionName[2]);
        }

        
        public async Task<Event> GetEventById(string Id) =>
            await _events.Find(m => m.Id == Id).FirstOrDefaultAsync();
        public async Task<List<Event>> GetEvents() => 
            await _events.Find(_ => true).ToListAsync();
        public async Task<List<Event>> GetEvents(Expression<Func<Event, bool>> filter) =>
            await _events.Find(filter).ToListAsync();
        public async Task<List<Event>> GetEventByType(EventType type) =>
            await this.GetEvents(e => e.Type == type);

        public async Task<List<Event>> GetEventByTag(string tagName) =>
            await this.GetEvents(e => e.Tags.Any(tag => tag.Name == tagName));

        public async Task SendMessage(string eventId, Message message) {
            Event currentEvent = await this.GetEventById(eventId);
            currentEvent.Messages.Append(message);

            await this.UpdateEvent(currentEvent.Id!, EventUpdate.fromEvent(currentEvent));
        }

        public async Task EditMessage(string eventId, DateTime indentifier, string new_message){
            Event currentEvent = await this.GetEventById(eventId);
            Message msg = currentEvent.Messages.Single(m => m.Date == indentifier);
            msg.Text = new_message;
            msg.Edit = DateTime.Now;

            
        }

        public async Task<Event> CreateEvent(EventUpdate createEventData) {
            Event newEvent = createEventData.toEvent();
            await _events.InsertOneAsync(newEvent);
            return newEvent;
        }

        // 
        public async Task UpdateEvent(string eventId, EventUpdate update) {
            // await _events.ReplaceOneAsync(m => m.Id == Id, updateEvent);
        }

        public async Task DeleteEventById(string Id) =>
            await _events.DeleteOneAsync(m => m.Id == Id);
    }
}