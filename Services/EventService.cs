using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TeamHunterBackend.DB;
using TeamHunterBackend.Schemas;

namespace TeamHunterBackend.Services
{
    public class MessageService
    {
        private readonly IMongoCollection<Event> _events;

        public MessageService(IOptions<DBSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _events = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<Event>(options.Value.CollectionName[2]);
        }

        public async Task<List<Event>> GetEvents() => 
            await _events.Find(_ => true).ToListAsync();
        
        public async Task<Event> GetEventById(int Id) =>
            await _events.Find(m => m.EventId == Id).FirstOrDefaultAsync();

        public async Task<List<Event>> GetEventByTypeOfEvent(int Id) =>
            await _events.Find(m => m.TypeOfEvent == Id).ToListAsync();

        public async Task<List<Event>> GetEventByTag(int Id) 
        {
            var _allEvents = await _events.Find(_ => true).ToListAsync();
            List<Event> result = new List<Event>();
            foreach (var _event in _allEvents)
            {
                foreach (var tag in _event.Tags!)
                {
                    if(tag == Id)
                    {
                        result.Add(_event);
                    }
                }
            }
            return result;
        }
        
        public async Task CreateEvent(Event newEvent) =>
            await _events.InsertOneAsync(newEvent);

        public async Task UpdateEvent(int Id, Event updateEvent) => 
            await _events.ReplaceOneAsync(m => m.EventId == Id, updateEvent);

        public async Task DeleteEventById(int Id) =>
            await _events.DeleteOneAsync(m => m.EventId == Id);
    }
}