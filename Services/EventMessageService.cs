using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TeamHunterBackend.DB;
using TeamHunterBackend.Schemas;

namespace TeamHunterBackend.Services
{
    public class EventMessageService
    {
        private readonly IMongoCollection<Message> _message;

        public EventMessageService(IOptions<DBSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _message = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<Message>(options.Value.CollectionName[4]);
        }

        public async Task<List<Message>> GetMessages() => 
            await _message.Find(_ => true).ToListAsync();
        
        public async Task<Message> GetMessageById(int Id) =>
            await _message.Find(m => m.MessageId == Id).FirstOrDefaultAsync();

        public async Task CreateMessage(Message newMessage) =>
            await _message.InsertOneAsync(newMessage);

        public async Task UpdateMessage(int Id, Message updateMessage) => 
            await _message.ReplaceOneAsync(m => m.MessageId == Id, updateMessage);

        public async Task DeleteMessageById(int Id) =>
            await _message.DeleteOneAsync(m => m.MessageId == Id);
    }
}