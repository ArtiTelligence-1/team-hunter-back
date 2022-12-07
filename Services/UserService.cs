using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TeamHunter.DB;
using TeamHunter.Schemas;

namespace TeamHunter.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IOptions<DBSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _users = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<User>(options.Value.CollectionName[0]);
        }

        public async Task<List<User>> GetUsers() => 
            await _users.Find(_ => true).ToListAsync();
        
        public async Task<User> GetUserById(int Id) =>
            await _users.Find(m => m.UserId == Id).FirstOrDefaultAsync();

        public async Task CreateUser(User newUser) =>
            await _users.InsertOneAsync(newUser);

        public async Task UpdateUser(int Id, User updateUser) => 
            await _users.ReplaceOneAsync(m => m.UserId == Id, updateUser);

        public async Task DeleteUserById(int Id) =>
            await _users.DeleteOneAsync(m => m.UserId == Id);
            
    }
}