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
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IGenerateIDService _generateID;

    public UserController(UserService userService, IGenerateIDService generateID)
    {
        _userService = userService;
        _generateID = generateID;
    }
        
    //[Authorize(Roles = "Admin")]
    [Authorize]
    [HttpGet("getUsers")]
    public async Task<List<User>> GetAllUsers() =>
        await _userService.GetUsers();

    [Authorize(Roles = "User, Admin")]
    [Authorize]
    [HttpGet("getUser/{Id}")]
    public async Task<ActionResult<User>> GetUserById(int Id)
    {
        var user = await _userService.GetUserById(Id);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser(User newUser)
    {
        newUser.UserId = _generateID.GenerateID("user_id");
        await _userService.CreateUser(newUser);

        return CreatedAtAction(nameof(GetUserById), new { Id = newUser.UserId }, newUser);
    }

    [Authorize(Roles = "User")]
    [HttpPut("UpdateUser/{Id}")]
    public async Task<IActionResult> UpdateUser(int Id, User updatedUser)
    {
        var user = await _userService.GetUserById(Id);

        if (user is null)
        {
            return NotFound();
        }

        updatedUser.UserId = user.UserId;

        await _userService.UpdateUser(Id, updatedUser);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteUser/{Id}")]
    public async Task<IActionResult> DeleteUser(int Id)
    {
        var user = await _userService.GetUserById(Id);

        if (user is null)
        {
            return NotFound();
        }

        await _userService.DeleteUserById(Id);

        return NoContent();
    }

    }
    
}