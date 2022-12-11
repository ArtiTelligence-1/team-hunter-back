using MongoDB.Driver;
using MongoDB.Bson;

using TeamHunter.Models.DTO;
using TeamHunter.Models;
using TeamHunter.Interfaces;

using System.Reflection;
using System.Net;

public class UserService : IUserService
{
    private readonly IMongoCollection<User> userManager;

    public UserService(IDBSessionManagerService manager) {
        userManager = manager.GetCollection<User>();
    }

    public async Task<UserShortInfo> CreateUserAwait(UserCreate userCreate) {
        User user = userCreate.ToUser();
        user.Id = ObjectId.GenerateNewId();

        await this.userManager.InsertOneAsync(user);
        
        return user;
    }

    protected async Task<IAsyncCursor<User>> GetUserCursorById(string userId){
        ObjectId userObjectId = new ObjectId(userId);
        return await userManager.FindAsync(user => user.Id == userObjectId);
    }

    public async Task<User> GetUserByIdAsync(string userId) =>
        (await this.GetUserCursorById(userId)).FirstOrDefault();

    public async Task<User> ModifyUserAwait(string userId, UserCreate userModify) {
        UpdateDefinitionBuilder<User> modification = Builders<User>.Update;
        User user = await this.GetUserByIdAsync(userId);
        PropertyInfo[] userModifyProperties = userModify.GetType().GetProperties();
        PropertyInfo[] userProperties = user.GetType().GetProperties();

        foreach(var item in userModify.GetType().GetProperties()){
            if (item.GetValue(userModify) is not null) {
                modification.AddToSet(item.Name, item.GetValue(userModify));

                userProperties.First(prop => prop.Name == item.Name)
                    .SetValue(user, item.GetValue(userModify));
            }
        }
    
        await this.userManager.FindOneAndUpdateAsync(u => u.Id == user.Id, modification.Combine());
        return user;
    }
    public async Task DeleteUserAsync(string userId) {
        ObjectId userObjectId = new ObjectId(userId);

        await this.userManager.DeleteOneAsync(u => u.Id == userObjectId);
    }
    public async Task<SessionInfo> CreateSessionAsync(string userId, IPAddress ipAddress, string userAgent) {
        User user = await this.GetUserByIdAsync(userId);
        SessionInfo session = new SessionInfo(){
            IPAddress = ipAddress,
            Agent = userAgent
        };
        user.ActiveSessions.Add(session);
        UpdateDefinition<User> update = Builders<User>.Update.Set("ActiveSessions", user.ActiveSessions);

        await this.userManager.UpdateOneAsync(u => u.Id == user.Id, update);
        return session;
    }
    public async Task DeleteSessionAsync(string userId, string sessionId) {
        User user = await this.GetUserByIdAsync(userId);
        ObjectId sessionObjectId = new ObjectId(sessionId);
        
        user.ActiveSessions.RemoveAt(user.ActiveSessions.FindIndex(s => s.Id == sessionObjectId));
        UpdateDefinition<User> update = Builders<User>.Update.Set("ActiveSessions", user.ActiveSessions);

        await this.userManager.UpdateOneAsync(u => u.Id == user.Id, update);
    }
}