using MongoDB.Driver;
using MongoDB.Bson;

using TeamHunter.Models.DTO;
using TeamHunter.Models;
using TeamHunter.Interfaces;

using System.Reflection;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

public class UserService : IUserService
{
    private readonly IMongoCollection<User> userManager;
    private readonly ICredentialsService _creadentialsManager;
    private readonly JwtSecurityTokenHandler _tokenManager;

    public UserService(IDBSessionManagerService manager, ICredentialsService credentialsService) {
        userManager = manager.GetCollection<User>();
        _creadentialsManager = credentialsService;
        _tokenManager = new JwtSecurityTokenHandler();
    }

    public async Task<UserShortInfo> CreateUserAsync(UserCreate userCreate) {
        User user = userCreate.ToUser();
        user.Id = ObjectId.GenerateNewId().ToString();

        await this.userManager.InsertOneAsync(user);
        
        return user;
    }

    protected async Task<User> GetUserAsync(string userId) =>
        (await userManager.FindAsync(user => user.Id == userId)).First();
    protected async Task<IAsyncCursor<User>> GetUserCursorById(string userId){
        return await userManager.FindAsync(user => user.Id == userId);
    }

    public async Task<User?> GetUserByIdAsync(string userId) =>
        (await this.GetUserCursorById(userId)).FirstOrDefault();

    public async Task<User?> GetUserByTelegramIdAsync(long userTelegramId) =>
        (await userManager.FindAsync(user => user.TelegramId == userTelegramId)).FirstOrDefault();

    public async Task<User> ModifyUserAsync(string userId, UserCreate userModify) {
        UpdateDefinitionBuilder<User> modification = Builders<User>.Update;
        List<UpdateDefinition<User>> modifiactionList = new List<UpdateDefinition<User>>();
        User user = (await this.GetUserByIdAsync(userId))!;
        PropertyInfo[] userModifyProperties = userModify.GetType().GetProperties();
        PropertyInfo[] userProperties = user.GetType().GetProperties();

        foreach(var item in userModify.GetType().GetProperties()){
            if (item.Name == "Id") continue;
            if (item.GetValue(userModify) is not null) {
                modifiactionList.Add(modification.Set(item.Name, item.GetValue(userModify)));
                // await this.userManager.FindOneAndUpdateAsync(u => u.Id == user.Id, mod);

                userProperties.First(prop => prop.Name == item.Name)
                    .SetValue(user, item.GetValue(userModify));
            }
        }
    
        await this.userManager.FindOneAndUpdateAsync(u => u.Id == user.Id, modification.Combine(modifiactionList));
        return user;
    }
    public async Task DeleteUserAsync(string userId) {
        await this.userManager.DeleteOneAsync(u => u.Id == userId);
    }
    public async Task<User?> GetUserFromSessionAsync(HttpRequest request){
        string authString = request.Headers.Authorization.First() ?? "";
        if (authString.Substring(0, 6) == "Bearer"){
            string token = authString.Substring(7);

            var validationResult = await _tokenManager.ValidateTokenAsync(token, new TokenValidationParameters{
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_creadentialsManager.TokenSecret)) 
            });

            if (validationResult.IsValid){
                return await GetUserByIdAsync(_tokenManager.ReadJwtToken(token).Payload["nameid"].ToString()!);
            }
        }
        return null;
    }
    public async Task<SessionInfo> CreateSessionAsync(string userId, IPAddress ipAddress, string userAgent) {
        User user = await this.GetUserAsync(userId);
        SessionInfo session = new SessionInfo(){
            IPAddress = ipAddress.ToString(),
            Agent = userAgent
        };
        user.ActiveSessions.Add(session);
        UpdateDefinition<User> update = Builders<User>.Update.Set("ActiveSessions", user.ActiveSessions);

        await this.userManager.UpdateOneAsync(u => u.Id == user.Id, update);
        return session;
    }

    public async Task DeleteSessionAsync(string userId, string sessionId) {
        User user = (await this.GetUserByIdAsync(userId))!;
        ObjectId sessionObjectId = new ObjectId(sessionId);
        
        user.ActiveSessions.RemoveAt(user.ActiveSessions.FindIndex(s => s.Id == sessionObjectId));
        UpdateDefinition<User> update = Builders<User>.Update.Set("ActiveSessions", user.ActiveSessions);

        await this.userManager.UpdateOneAsync(u => u.Id == user.Id, update);
    }
}