using MongoDB.Driver;
using Microsoft.Extensions.Options;
using TeamHunterBackend.DB;
using TeamHunterBackend.Schemas;

namespace TeamHunterBackend.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IMongoCollection<JwtRefreshToken> _refreshTokens;

        public UserTokenService(IOptions<DBSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _refreshTokens = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<JwtRefreshToken>(options.Value.CollectionName[7]);
        }
        
        public async Task AddUserRefreshToken(JwtRefreshToken token) =>
            await _refreshTokens.InsertOneAsync(token);

        public async Task DeleteUserRefreshToken(string userId, string refreshToken) =>
            await _refreshTokens.DeleteOneAsync(m => m.UserId!.ToString() == userId && m.RefreshToken == refreshToken);
        
        public async Task<JwtRefreshToken> GetSavedRefreshTokens(string userId, string refreshToken) =>
            await _refreshTokens.Find(m => m.UserId!.ToString() == userId && m.RefreshToken == refreshToken).FirstOrDefaultAsync();
    }
}