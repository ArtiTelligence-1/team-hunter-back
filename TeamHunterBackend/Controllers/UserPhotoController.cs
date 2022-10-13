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
    public class UserPhotoController : ControllerBase
    {
        private readonly UserPhotoService _userPhotoService;

    public UserPhotoController(UserPhotoService userService) =>
        _userPhotoService = userService;

    [HttpGet("GetUserPhoto/{Id}")]
    public async Task<ActionResult<UserPhoto>> GetUserPhotoById(int Id)
    {
        var userPhoto = await _userPhotoService.GetUserPhotoById(Id);

        if (userPhoto is null)
        {
            return NotFound();
        }

        return userPhoto;
    }

    [HttpPost("AddUserPhoto")]
    public async Task<IActionResult> AddUserPhoto(UserPhoto newUserPhoto)
    {
        await _userPhotoService.AddUserPhoto(newUserPhoto);

        return CreatedAtAction(nameof(GetUserPhotoById), new { Id = newUserPhoto.PhotoId }, newUserPhoto);
    }

    [HttpPut("UpdateUserPhoto/{Id}")]
    public async Task<IActionResult> UpdateUserPhotoById(int Id, UserPhoto updatedUserPhoto)
    {
        var userPhoto = await _userPhotoService.GetUserPhotoById(Id);

        if (userPhoto is null)
        {
            return NotFound();
        }

        updatedUserPhoto.PhotoId = userPhoto.PhotoId;

        await _userPhotoService.UpdateUserPhotoById(Id, updatedUserPhoto);

        return NoContent();
    }

    [HttpDelete("DeleteUserPhoto/{Id}")]
    public async Task<IActionResult> DeleteUserPhotoById(int Id)
    {
        var user = await _userPhotoService.GetUserPhotoById(Id);

        if (user is null)
        {
            return NotFound();
        }

        await _userPhotoService.DeleteUserPhotoById(Id);

        return NoContent();
    }
    
    }
}