using TeamHunter.Models;
using TeamHunter.Models.DTO;

namespace TeamHunter.Interfaces;

public interface IUserService
{
    Task<UserShortInfo> CreateUser(UserCreate userCreate);
    Task<User> GetUserById(string userId);
    Task<User> ModifyUser(string userId, UserCreate userModify);
    Task DeleteUser(string userId);
    Task<SessionInfo> CreateSession(string userId);
    Task DeleteSession(string userId, string sessionId);


}