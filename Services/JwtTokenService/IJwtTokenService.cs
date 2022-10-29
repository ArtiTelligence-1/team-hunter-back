using System.Security.Claims;
using TeamHunterBackend.Models;

namespace TeamHunterBackend.Services
{
    public interface IJwtTokenService
    {
        public JwtToken GenerateJwtTokens(string userId);
        public ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}