using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TeamHunterBackend.Services;

namespace TeamHunterBackend.Controllers 
{
    [Route("account")]
    public class AccountController : Controller
    {
        private IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var account = _accountService.Login(username, password);
            if(account != null)
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(_accountService.GetClaimsAccount(account), 
                    CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return RedirectToAction("welcome");
            }
            else
            {
                ViewBag.msg = "Invalid";
                return View("Login");
            }
        }
        [Route("Welcome")]
        [HttpGet]
        public IActionResult Welcome()
        {
            var account = User.FindFirst(ClaimTypes.Name);
            if(account != null){
                ViewBag.username = account.Value;
            }
            else
            {
                ViewBag.username = "User";
            }
            return View("Welcome");
        }
        [Route("logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("login");
        }
    }
}