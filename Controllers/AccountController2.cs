using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TeamHunterBackend.Models;
using TeamHunterBackend.Schemas;
using TeamHunterBackend.Services;

namespace TeamHunterBackend.Controllers
{
    [Authorize]
    [Route("MyAuth/")]
    [ApiController]
    [EnableCors("Policy")]
    public class AccountController2 : ControllerBase
    {

	private readonly IJwtTokenService _JwtService;
	private readonly IUserTokenService _userTokenService;
	private readonly IUserCredentialService _userCredentialService;

	public AccountController2(IJwtTokenService JwtService, IUserTokenService userTokenService, IUserCredentialService userCredentialService)
	{
		_JwtService = JwtService;
		_userTokenService = userTokenService;
		_userCredentialService = userCredentialService;
	}

	[HttpGet]
	public List<string> Get()
	{
		var users = new List<string>
		{
			"Satinder Singh",
			"Amit Sarna",
			"Davin Jon"
		};

		return users;
	}

	[AllowAnonymous]
	[HttpPost]
	[Route("authenticate")]
	public async Task<IActionResult> AuthenticateAsync(UserLogin userLogin)
	{
		var validUser = _userCredentialService.IsValidCredential(userLogin);

		if (validUser is null)
		{
			return Unauthorized("Incorrect username or password!");
		}

		var token = _JwtService.GenerateJwtTokens(userLogin.PhoneNumber!);

		if (token == null)
		{
			return Unauthorized("Invalid Attempt!");
		}

		// saving refresh token to the db
		JwtRefreshToken obj = new JwtRefreshToken
		{     
			UserId = userLogin.PhoneNumber,          
			RefreshToken = token.RefreshToken
		};

		await _userTokenService.AddUserRefreshToken(obj);
		return Ok(token);
	}

	[AllowAnonymous]
	[HttpPost]
	[Route("refresh")]
	public async Task<ActionResult<JwtToken>> Refresh(JwtToken token)
	{
		var principal = _JwtService.GetPrincipalFromToken(token.AccessToken!);
		var userId = principal.Identity?.Name;

		//retrieve the saved refresh token from database
		var savedRefreshToken =  await _userTokenService.GetSavedRefreshTokens(userId!, token.RefreshToken!);

		if (savedRefreshToken == null)
		{
			return Unauthorized("Invalid attempt!");
		}

		var newJwtToken = _JwtService.GenerateJwtTokens(userId!);

		if (newJwtToken == null)
		{
			return Unauthorized("Invalid attempt!");
		}

		// saving refresh token to the db
		JwtRefreshToken obj = new JwtRefreshToken
		{
			RefreshToken = newJwtToken.RefreshToken,
			UserId = userId
		};

		await _userTokenService.DeleteUserRefreshToken(userId!, token.RefreshToken!);
		await _userTokenService.AddUserRefreshToken(obj);

		return Ok(newJwtToken);
	}

    }
}