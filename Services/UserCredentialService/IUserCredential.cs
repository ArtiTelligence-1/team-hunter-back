using TeamHunterBackend.Models;
using TeamHunterBackend.Schemas;

namespace TeamHunterBackend.Services
{
    public interface IUserCredentialService
    {
        public Task<List<UserCredential>> GetUserCredentials();
        public Task<UserCredential> GetUserCredentialById(string userId);
        public Task CreateUserCredential(UserCredential newUserCred);
        public Task UpdateUserCredential(string userId, UserCredential updateUserCred);
        public Task DeleteUserCredentialById(string userId);
        public Task IsValidCredential(UserLogin userCred);
    }
}