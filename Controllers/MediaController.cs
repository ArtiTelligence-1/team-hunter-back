// using Microsoft.AspNetCore.Cors;
// using Microsoft.AspNetCore.Mvc;
// using TeamHunter.Models;
// using TeamHunter.Services;

// namespace TeamHunter.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]
//     [EnableCors("Policy")]
//     //[DisableCors]
//     public class MediaController : ControllerBase
//     {
//         private readonly UserPhotoService _userPhotoService;

//     public MediaController(UserPhotoService userService) =>
//         _userPhotoService = userService;

//     [HttpPost("photo")]
//     public async Task<IActionResult> AddPhoto(Photo newUserPhoto)
//     {
//         await _userPhotoService.AddUserPhoto(newUserPhoto);

//         return CreatedAtAction(nameof(GetPhoto), new { Id = newUserPhoto.Id }, newUserPhoto);
//     }

//     [HttpGet("photo/{Id}")]
//     public async Task<ActionResult<Photo>> GetPhoto(int Id)
//     {
//         var userPhoto = await _userPhotoService.GetUserPhotoById(Id);

//         if (userPhoto is null)
//         {
//             return NotFound();
//         }

//         return userPhoto;
//     }

//     [HttpPut("photo/{Id}")]
//     public async Task<IActionResult> UpdateUserPhotoById(int Id, Photo updatedUserPhoto)
//     {
//         var userPhoto = await _userPhotoService.GetUserPhotoById(Id);

//         if (userPhoto is null)
//         {
//             return NotFound();
//         }

//         updatedUserPhoto.Id = userPhoto.Id;

//         await _userPhotoService.UpdateUserPhotoById(Id, updatedUserPhoto);

//         return NoContent();
//     }

//     [HttpDelete("photo/{Id}")]
//     public async Task<IActionResult> DeleteUserPhotoById(int Id)
//     {
//         var user = await _userPhotoService.GetUserPhotoById(Id);

//         if (user is null)
//         {
//             return NotFound();
//         }

//         await _userPhotoService.DeleteUserPhotoById(Id);

//         return NoContent();
//     }
    
//     }
// }