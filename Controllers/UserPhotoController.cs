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
    public class UserPhotoController : ControllerBase
    {
        private readonly UserPhotoService _userPhotoService;
        private readonly IGenerateIDService _generateID;

    public UserPhotoController(UserPhotoService userPhotoService, IGenerateIDService generateID)
    {
        _userPhotoService = userPhotoService;
        _generateID = generateID;
    }
    // [Authorize(Roles = "Admin")]
    [HttpGet("GetUserPhoto/{Id}")]
    public async Task<ActionResult<UserPhoto>> GetUserPhotoById(int Id)
    {
        var userPhoto = await _userPhotoService.GetUserPhotoById(Id);

        if (userPhoto is null)
        {
            return NotFound();
        }
        return Redirect(userPhoto.Photo!);
    }
    
    [Authorize(Roles = "User")]
    [HttpPost("AddUserPhoto")]
    public async Task<IActionResult> AddUserPhoto(UserPhoto newUserPhoto)
    {
        newUserPhoto.PhotoId = _generateID.GenerateID("userPhoto_id");
        await _userPhotoService.AddUserPhoto(newUserPhoto);

        return CreatedAtAction(nameof(GetUserPhotoById), new { Id = newUserPhoto.PhotoId }, newUserPhoto);
    }

    [Authorize(Roles = "User")]
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

    [Authorize(Roles = "Admin")]
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