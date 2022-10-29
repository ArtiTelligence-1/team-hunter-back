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
    public class EventTagController : ControllerBase
    {
        private readonly EventTagService _eventTagService;
        private readonly IGenerateIDService _generateID;

    public EventTagController(EventTagService eventTagService, IGenerateIDService generateID)
    {
        _eventTagService = eventTagService;
        _generateID = generateID;
    }
        

    [Authorize]
    [HttpGet("getEventTags")]
    public async Task<List<EventTag>> GetAllEventTags() =>
        await _eventTagService.GetEventTags();

    [Authorize]
    [HttpGet("getEventTag/{Id}")]
    public async Task<ActionResult<EventTag>> GetEventTagById(int Id)
    {
        var _eventTag = await _eventTagService.GetEventTagById(Id);

        if (_eventTag is null)
        {
            return NotFound();
        }

        return _eventTag;
    }

    [Authorize]
    [HttpPost("CreateEventTag")]
    public async Task<IActionResult> CreateEventTag(EventTag newEventTag)
    {
        newEventTag.EventTagId = _generateID.GenerateID("eventTag_id");
        await _eventTagService.CreateEventTag(newEventTag);

        return CreatedAtAction(nameof(GetEventTagById), new { Id = newEventTag.EventTagId }, newEventTag);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteEventTag/{Id}")]
    public async Task<IActionResult> DeleteEventTag(int Id)
    {
        var user = await _eventTagService.GetEventTagById(Id);

        if (user is null)
        {
            return NotFound();
        }

        await _eventTagService.DeleteEventTagById(Id);

        return NoContent();
    }
    }
}