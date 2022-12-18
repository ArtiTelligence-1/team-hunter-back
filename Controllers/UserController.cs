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
using WebApi.Jwt.Filters;

namespace TeamHunter.Controllers;

[Route("/api/v1/user")]
[EnableCors("Policy")]
[ApiController]
public class UserController: ControllerBase {
    private readonly IUserService _userManger;
    private readonly ICredentialsService _credentialsManager;
    private readonly JwtSecurityTokenHandler _tokenManager;

    public UserController(
            IUserService userService,
            ICredentialsService applicationCredentialsService
    ){
        _userManger = userService;
        _credentialsManager = applicationCredentialsService;
        _tokenManager = new JwtSecurityTokenHandler();
    }

    [HttpGet]
    [Authorize]
    public async Task<User?> GetUser(){
        return await _userManger.GetUserFromSession(Request);
    }

    [HttpPut]
    [Authorize]
    public async Task<UserCreate> ModifyUser([FromBody]UserCreate userModify) {
        User? user = await _userManger.GetUserFromSession(Request);
        return UserCreate.fromUser(await _userManger.ModifyUserAsync(user!.Id.ToString()!, userModify));
    }

    [HttpPost("oauth/telegram")]
    public async Task<IActionResult> TelegrmOauth([FromBody]TelegramOauthModel authModel){
        string dataStr = $"auth_date={authModel.auth_date}\nfirst_name={authModel.first_name}\nid={authModel.id}\nusername={authModel.username}";
        Console.WriteLine(dataStr);
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

            if (user is null) {
                user = await _userManger.CreateUserAsync(new UserCreate {
                    TelegramId = authModel.id,
                    FirstName = authModel.first_name,
                    LastName = authModel.last_name ?? "",
                    PhotoUrl = authModel.photo_url
                });
            }

            SessionInfo session = await _userManger.CreateSessionAsync(
                user.Id! ?? MongoDB.Bson.ObjectId.Empty,
                HttpContext.Connection.RemoteIpAddress!,
                Request.Headers.UserAgent.First() ?? "");

            SigningCredentials singer = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(
                        _credentialsManager.TokenSecret
                    )
                ),
                SecurityAlgorithms.HmacSha512Signature
            );
            return Ok(new TokenModel(){
                access_token = _tokenManager.CreateEncodedJwt(new SecurityTokenDescriptor{
                    Expires = DateTime.Now.AddDays(4),
                    IssuedAt = DateTime.Now,
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
                        new Claim("session", session.Id.ToString()),
                        new Claim("type", "au")
                    }),
                    SigningCredentials = singer
                }),
                refresh_token = _tokenManager.CreateEncodedJwt(new SecurityTokenDescriptor{
                    Expires = DateTime.Now.AddHours(8),
                    IssuedAt = DateTime.Now,
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
                        new Claim("session", session.Id.ToString()),
                        new Claim("type", "re")
                    }),
                    SigningCredentials = singer
                }),
            });
        }
        return BadRequest("hash error");
    }

    private byte[] ShaHash(String value) =>
        SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value));

    private  byte[] HashHmac(byte[] key, byte[] message) =>
        new HMACSHA256(key).ComputeHash(message);
    
    private string GenerateToken(string userId)
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
                new Claim(ClaimTypes.NameIdentifier, userId),
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