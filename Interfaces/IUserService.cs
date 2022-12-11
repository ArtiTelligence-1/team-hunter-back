using TeamHunter.Models;
using TeamHunter.Models.DTO;

using System.Net;

namespace TeamHunter.Interfaces;

public interface IUserService
{
    Task<UserShortInfo> CreateUserAwait(UserCreate userCreate);
    Task<User> GetUserByIdAsync(string userId);
    Task<User> ModifyUserAwait(string userId, UserCreate userModify);
    Task DeleteUserAsync(string userId);
    Task<SessionInfo> CreateSessionAsync(string userId, IPAddress ipAddress, string userAgent);
    Task DeleteSessionAsync(string userId, string sessionId);


}