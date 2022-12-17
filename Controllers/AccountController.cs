// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using System.Security.Claims;
// using System.Text;
// using TeamHunterBackend.Schemas;
// using System.Security.Cryptography;
// using System.IdentityModel.Tokens.Jwt;
// using Microsoft.IdentityModel.Tokens;
// using TeamHunterBackend.Services;

// namespace TeamHunterBackend.Controllers
// {

//     [ApiController]
//     [Authorize]
//     [Route("[controller]")]
//     public class AccountController : ControllerBase
//     {
//         private readonly UserService _userService;
//         public AccountController(UserService userService)
//         {
//              _userService = userService;
//         }

//         [HttpPost("TelegramExternalLogin")]
//         [AllowAnonymous]
//         public async Task<IActionResult> TelegramExternalLogin([FromBody] UserTelegram user)
//         {
//             string dataStr = $"auth_date={user.AuthDate}\nfirst_name={user.FirstName}\nid={user.Id}username={user.UserName}";
//             // byte[] signature = _hmac.ComputeHash(Encoding.UTF8.GetBytes(dataStringBuilder.ToString()));

//             var secretKey = ShaHash("5672519330:AAEp2OI15qb8hHOv1UbfJZQaKcAMCUzIeys");

//             var myHash = HashHmac(secretKey, Encoding.UTF8.GetBytes(dataStr));

//             var teltime = 0;
//             teltime = user.AuthDate;
//             var time = DateTimeOffset.Now.ToUnixTimeSeconds(); 
            

//             if (time - teltime > 86400)// 24 hours
//             {
//                return BadRequest("Data is outdated");
//             }
            
//             var myHashStr = String.Concat(myHash.Select(i => i.ToString("x2")));
//             var providerKey = user.Id;
//             if (myHashStr == user.Hash)
//             {   
//                 var user_tel = _userService.GetUserById(providerKey);

//                 if (user_tel == null)
//                 {
//                     var newUser = new User{UserId = user.Id, Name = user.FirstName};
//                     await _userService.CreateUser(newUser);
//                     user_tel = _userService.GetUserById(newUser.UserId);
//                 }
//                 else
//                 {
//                     var accessToken = GenerateToken(user_tel.Id);
//                     var refreshToken = "";
//                     return Ok(new Token { access_token = accessToken, refresh_token = refreshToken });
//                 }
//             }
//             return BadRequest("hash error");
//         }

//         private  byte[] ShaHash(String value) { 
//             using (var hasher = SHA256.Create()) 
            
//             { return hasher.ComputeHash(Encoding.UTF8.GetBytes(value)); } 
//         }

//         private  byte[] HashHmac(byte[] key, byte[] message)
//         {
//             var hash = new HMACSHA256(key);
//             return hash.ComputeHash(message);
//         }

        
//         private string GenerateToken(int userId)
//         {
// 	        var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
// 	        var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

// 	        var myIssuer = "http://mysite.com";
// 	        var myAudience = "http://myaudience.com";

// 	        var tokenHandler = new JwtSecurityTokenHandler();
// 	        var tokenDescriptor = new SecurityTokenDescriptor
// 	        {
// 		        Subject = new ClaimsIdentity(new Claim[]
// 		        {
// 			        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
// 		        }),
// 		        Expires = DateTime.UtcNow.AddDays(7),
// 		        Issuer = myIssuer,
// 		        Audience = myAudience,
// 		        SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
// 	        };

// 	        var token = tokenHandler.CreateToken(tokenDescriptor);
// 	        return tokenHandler.WriteToken(token);
//         }
        

//     }
// }

// /*
//     {
//       "id":607943839,
//       "first_name":"Роман",
//       "username":"rmnkndrt",
//       "photo_url":"https:\/\/t.me\/i\/userpic\/320\/cUoNyqzfzgSxo9ERF0Kxk1Uarer_sWafeQiGErg6m88.jpg",
//       "auth_date":1668938901,
//       "hash":"93bd9eb529e990381cbb45665a6f4f6f3c776d66ba5091d9ef6df61c15ce12f7"
//     }


//     {
//         "id":607943839,
//         "first_name":"Роман",
//         "username":"rmnkndrt",
//         "photo_url":"https:\/\/t.me\/i\/userpic\/320\/cUoNyqzfzgSxo9ERF0Kxk1Uarer_sWafeQiGErg6m88.jpg",
//         "auth_date":1669741699,
//         "hash":"aa7648f0ee04a7ec80f747c71f434906e8a2de62c9f66c85107bbec58e8a804a"
//     }
// */