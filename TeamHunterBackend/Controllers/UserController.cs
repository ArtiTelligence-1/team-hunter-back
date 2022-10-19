using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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

    public UserController(UserService userService) =>
        _userService = userService;
        
    // [Route("/")]
    // [HttpGet]
    // public string MainPage()
    // {
    //     return "Hello World!";
    // }
    //[EnableCors("Policy")]
    [HttpGet("getUsers")]
    public async Task<List<User>> GetAllUsers() =>
        await _userService.GetUsers();

    //[EnableCors("Policy")]
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

    //[EnableCors("Policy")]
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser(User newUser)
    {
        await _userService.CreateUser(newUser);

        return CreatedAtAction(nameof(GetUserById), new { Id = newUser.UserId }, newUser);
    }

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