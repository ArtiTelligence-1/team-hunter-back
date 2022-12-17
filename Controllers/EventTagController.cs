// using Microsoft.AspNetCore.Cors;
// using Microsoft.AspNetCore.Mvc;
// using TeamHunter.Models;
// using TeamHunter.Services;

// namespace TeamHunter.Controllers 
// {
//     [ApiController]
//     [Route("/api/v1/[controller]")]
//     [EnableCors("Policy")]
//     //[DisableCors]
//     public class EventTagController : ControllerBase
//     {
//         private readonly EventTagService _eventTagService;

//     public EventTagController(EventTagService eventTagService) =>
//         _eventTagService = eventTagService;

//     [HttpGet("getEventTags")]
//     public async Task<List<EventTag>> GetAllEventTags() =>
//         await _eventTagService.GetEventTags();

//     [HttpGet("getEventTag/{Id}")]
//     public async Task<ActionResult<EventTag>> GetEventTagById(int Id)
//     {
//         var _eventTag = await _eventTagService.GetEventTagById(Id);

//         if (_eventTag is null)
//         {
//             return NotFound();
//         }

//         return _eventTag;
//     }

//     [HttpPost("CreateEventTag")]
//     public async Task<IActionResult> CreateEventTag(EventTag newEventTag)
//     {
//         await _eventTagService.CreateEventTag(newEventTag);

//         return CreatedAtAction(nameof(GetEventTagById), new { Id = newEventTag.EventTagId }, newEventTag);
//     }

//     [HttpDelete("DeleteEventTag/{Id}")]
//     public async Task<IActionResult> DeleteEventTag(int Id)
//     {
//         var user = await _eventTagService.GetEventTagById(Id);

//         if (user is null)
//         {
//             return NotFound();
//         }

//         await _eventTagService.DeleteEventTagById(Id);

//         return NoContent();
//     }
//     }
// }