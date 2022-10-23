using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TeamHunterBackend.DB;
using TeamHunterBackend.Schemas;

namespace TeamHunterBackend.Services
{
    public class ChatService : IChatService
    {
        private readonly IMongoCollection<Chat> _chats;

        public ChatService(IOptions<DBSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _chats = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<Chat>(options.Value.CollectionName[3]);
        }

        public async Task<List<Chat>> GetChats() => 
            await _chats.Find(_ => true).ToListAsync();

        public async Task<Chat> GetChatById(int Id) =>
            await _chats.Find(m => m.ChatId == Id).FirstOrDefaultAsync();

        public async Task CreateChat(Chat newChat) =>
            await _chats.InsertOneAsync(newChat);

        public async Task UpdateChat(int Id, Chat updateChat) => 
            await _chats.ReplaceOneAsync(m => m.ChatId == Id, updateChat);

        public async Task DeleteChatById(int Id) =>
            await _chats.DeleteOneAsync(m => m.ChatId == Id);
    }
}