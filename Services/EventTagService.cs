using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TeamHunter.DB;
using TeamHunter.Schemas;

namespace TeamHunter.Services
{
    public class EventTagService
    {
        private readonly IMongoCollection<EventTag> _eventTags;

        public EventTagService(IOptions<DBSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _eventTags = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<EventTag>(options.Value.CollectionName[5]);
        }

        public async Task<List<EventTag>> GetEventTags() => 
            await _eventTags.Find(_ => true).ToListAsync();
        
        public async Task<EventTag> GetEventTagById(int Id) =>
            await _eventTags.Find(m => m.EventTagId == Id).FirstOrDefaultAsync();

        public async Task CreateEventTag(EventTag newEventTag) =>
            await _eventTags.InsertOneAsync(newEventTag);

        public async Task DeleteEventTagById(int Id) =>
            await _eventTags.DeleteOneAsync(m => m.EventTagId == Id);
    }
}