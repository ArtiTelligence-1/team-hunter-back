using TeamHunterBackend.Schemas;

namespace TeamHunterBackend.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetUsers();
        public  Task<User> GetUserById(int Id);
        public  Task CreateUser(User newUser);
        public  Task UpdateUser(int Id, User updateUser);
        public  Task DeleteUserById(int Id);
    }
}