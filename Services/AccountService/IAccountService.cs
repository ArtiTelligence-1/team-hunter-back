using System.Security.Claims;
using TeamHunterBackend.Models;

namespace TeamHunterBackend.Services
{
    public interface IAccountService
    {
        public Account Login(string username, string password);
        public IEnumerable<Claim> GetClaimsAccount(Account account);
    }
}