using TeamHunterBackend.Schemas;

namespace TeamHunterBackend.Services
{
    public interface IUserTokenService
	{
		public  Task AddUserRefreshToken(JwtRefreshToken token);
		public  Task DeleteUserRefreshToken(string userId, string refreshToken);
		public  Task<JwtRefreshToken> GetSavedRefreshTokens(string userId, string refreshToken);
	}
}