using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TeamHunter.Interfaces;
using TeamHunter.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using TeamHunterBackend.Schemas;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using TeamHunter.Models;

namespace TeamHunter.Controllers;

[Route("/api/v1/user")]
[ApiController]
public class UserController: ControllerBase {
    private readonly IUserService _userManger;
    private readonly ICredentialsService _credentialsManager;

    public UserController(IUserService userService, ICredentialsService applicationCredentialsService){
        _userManger = userService;
        _credentialsManager = applicationCredentialsService;
    }

    [HttpPost("oauth/telegram")]
    public async Task<IActionResult> TelegrmOauth(TelegramOauthModel authModel){
        string dataStr = $"auth_date={authModel.auth_date}\nfirst_name={authModel.first_name}\nid={authModel.id}username={authModel.username}";
        // byte[] signature = _hmac.ComputeHash(Encoding.UTF8.GetBytes(dataStringBuilder.ToString()));

        var secretKey = ShaHash(_credentialsManager.TelegramBotToken);
        var authHash = HashHmac(secretKey, Encoding.UTF8.GetBytes(dataStr));
        var secondsNow = DateTimeOffset.Now.ToUnixTimeSeconds(); 


        if (secondsNow - authModel.auth_date > 24 * 60 * 60) {
            return BadRequest(new { code = Response.StatusCode, message = ""});
        }
        
        var myHashStr = String.Concat(authHash.Select(i => i.ToString("x2")));
        Console.WriteLine(myHashStr);
        if (myHashStr == authModel.hash)
        {
            UserShortInfo? user = await _userManger.GetUserByTelegramIdAsync(authModel.id);

            if (user == null)
            {
                user = await _userManger.CreateUserAsync(new UserCreate {
                    TelegramId = authModel.id,
                    FirstName = authModel.first_name,
                    LastName = authModel.last_name,
                    PhotoUrl = authModel.photo_url
                });
            //     var newUser = new User{UserId = authModel.id, Name = authModel.first_name};
            //     await _userManger.CreateUser(newUser);
            //     user = _userManger.GetUserById(newUser.UserId);
            }
            


            return Ok(user == null);
        }
        return BadRequest("hash error");
    }

    private  byte[] ShaHash(String value) { 
        using (var hasher = SHA256.Create()) 
        
        { return hasher.ComputeHash(Encoding.UTF8.GetBytes(value)); } 
    }

    private  byte[] HashHmac(byte[] key, byte[] message)
    {
        var hash = new HMACSHA256(key);
        return hash.ComputeHash(message);
    }

    private string GenerateToken(int userId)
    {
        var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
        var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

        var myIssuer = "http://mysite.com";
        var myAudience = "http://myaudience.com";

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = myIssuer,
            Audience = myAudience,
            SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}