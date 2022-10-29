using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TeamHunterBackend.DB;
using TeamHunterBackend.Models;
using TeamHunterBackend.Schemas;

namespace TeamHunterBackend.Services
{
    public class UserCredentialService : IUserCredentialService
    {
        private readonly IMongoCollection<UserCredential> _usersCred;
        public UserCredentialService(IOptions<DBSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _usersCred = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<UserCredential>(options.Value.CollectionName[8]);
        }

        public async Task<List<UserCredential>> GetUserCredentials() => 
            await _usersCred.Find(_ => true).ToListAsync();
        
        public async Task<UserCredential> GetUserCredentialById(string userId) =>
            await _usersCred.Find(m => m.UserId == userId).FirstOrDefaultAsync();

        public async Task CreateUserCredential(UserCredential newUserCred) =>
            await _usersCred.InsertOneAsync(newUserCred);
            
        public async Task UpdateUserCredential(string userId, UserCredential updateUserCred) => 
            await _usersCred.ReplaceOneAsync(m => m.UserId == userId, updateUserCred);

        public async Task DeleteUserCredentialById(string userId) =>
            await _usersCred.DeleteOneAsync(m => m.UserId== userId);    

        public async Task IsValidCredential(UserLogin userLogin) =>
            await _usersCred.Find(m => m.PhoneNumber == userLogin.PhoneNumber && m.Password == userLogin.Password).FirstOrDefaultAsync();	        
    }
}