using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TeamHunter.DB;
using TeamHunter.Schemas;
using System.Linq.Expressions;

namespace TeamHunter.Services
{
    public class MessageService
    {
        private readonly IMongoCollection<Event> _events;

        public MessageService(IOptions<DBSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _events = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<Event>(options.Value.CollectionName[2]);
        }

        
        public async Task<Event> GetEventById(int Id) =>
            await _events.Find(m => m.Id == Id).FirstOrDefaultAsync();
        public async Task<List<Event>> GetEvents() => 
            await _events.Find(_ => true).ToListAsync();
        public async Task<List<Event>> GetEvents(Expression<Func<Event, bool>> filter) =>
            await _events.Find(filter).ToListAsync();
        public async Task<List<Event>> GetEventByType(EventType type) =>
            await this.GetEvents(e => e.Type == type);

        public async Task<List<Event>> GetEventByTag(string tagName) =>
            await this.GetEvents(e => e.Tags.Any(tag => tag.Name == tagName));

        public async Task CreateEvent(Event newEvent) =>
            await _events.InsertOneAsync(newEvent);

        public async Task UpdateEvent(int Id, Event updateEvent) => 
            await _events.ReplaceOneAsync(m => m.Id == Id, updateEvent);

        public async Task DeleteEventById(int Id) =>
            await _events.DeleteOneAsync(m => m.Id == Id);
    }
}