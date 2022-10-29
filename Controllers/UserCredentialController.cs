using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TeamHunterBackend.DB;
using TeamHunterBackend.Schemas;
using TeamHunterBackend.Services;

namespace TeamHunterBackend.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("Policy")]
    //[DisableCors]
    public class UserCredentialController : ControllerBase
    {
        private readonly UserCredentialService _userCredService;
        private readonly IGenerateIDService _generateID;

    public UserCredentialController(UserCredentialService userCredService, IGenerateIDService generateID)
    {
        _userCredService = userCredService;
        _generateID = generateID;
    }
        
    // [Authorize(Roles = "Admin")]
    [HttpGet("getUserCredentials")]
    public async Task<List<UserCredential>> GetAllUserCredentials() =>
        await _userCredService.GetUserCredentials();

    // [Authorize(Roles = "User, Admin")]
    // [Authorize]
    [HttpGet("GetUserCredential/{Id}")]
    public async Task<ActionResult<UserCredential>> GetUserCredentialById(string userId)
    {
        var userCred = await _userCredService.GetUserCredentialById(userId);

        if (userCred is null)
        {
            return NotFound();
        }

        return userCred;
    }

    [HttpPost("CreateUserCredential")]
    public async Task<IActionResult> CreateUser(UserCredential newUserCred)
    {
        newUserCred.CredId = _generateID.GenerateID("userCred_id");
        await _userCredService.CreateUserCredential(newUserCred);

        return CreatedAtAction(nameof(GetUserCredentialById), new { Id = newUserCred.UserId }, newUserCred);
    }

    // [Authorize(Roles = "User")]
    [HttpPut("UpdateUserCredential/{Id}")]
    public async Task<IActionResult> UpdateUserCredentialById(string userId, UserCredential updatedUser)
    {
        var user = await _userCredService.GetUserCredentialById(userId);

        if (user is null)
        {
            return NotFound();
        }

        updatedUser.UserId = user.UserId;

        await _userCredService.UpdateUserCredential(userId, updatedUser);

        return NoContent();
    }

    // [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteUserCredential/{Id}")]
    public async Task<IActionResult> DeleteUserCredential(string userId)
    {
        var user = await _userCredService.GetUserCredentialById(userId);

        if (user is null)
        {
            return NotFound();
        }

        await _userCredService.DeleteUserCredentialById(userId);

        return NoContent();
    }

    }
    
}