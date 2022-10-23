using System.Security.Claims;
using TeamHunterBackend.Models;

namespace TeamHunterBackend.Services
{
    public class AccountService : IAccountService
    {
        private List<Account> _accounts;
        public AccountService()
        {
            _accounts = new List<Account>()
            {
                new Account{
                    Username = "Daminik",
                    Password = "2022",
                    Fullname = "Dima",
                    Roles = new List<string>
                    {
                        "User"
                    }
                },
                new Account{
                    Username = "Admin",
                    Password = "1111",
                    Fullname = "Admin",
                    Roles = new List<string>
                    {
                        "Admin"
                    }
                }
            };
        }
        public IEnumerable<Claim> GetClaimsAccount(Account account)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, account.Username));
            foreach (var role in account.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        public Account Login(string username, string password)
        {
            return _accounts.SingleOrDefault(a => a.Username == username && a.Password == password);
        }
    }
}