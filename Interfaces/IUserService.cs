using TeamHunter.Models;
using TeamHunter.Models.DTO;

using System.Net;
using MongoDB.Bson;

namespace TeamHunter.Interfaces;

public interface IUserService
{
    Task<UserShortInfo> CreateUserAsync(UserCreate userCreate);
    Task<User?> GetUserByIdAsync(string userId);
    Task<User?> GetUserByTelegramIdAsync(long userTelegramId);
    Task<User> ModifyUserAsync(string userId, UserCreate userModify);
    Task DeleteUserAsync(string userId);
    Task<User?> GetUserFromSession(HttpRequest request);
    Task<SessionInfo> CreateSessionAsync(ObjectId userId, IPAddress ipAddress, string userAgent);
    Task DeleteSessionAsync(string userId, string sessionId);
}