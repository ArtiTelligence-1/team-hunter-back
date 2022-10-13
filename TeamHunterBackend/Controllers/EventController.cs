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
    public class EventController : ControllerBase
    {
        private readonly MessageService _eventService;

    public EventController(MessageService eventService) =>
        _eventService = eventService;

    [HttpGet("getEvents")]
    public async Task<List<Event>> GetAllEvents() =>
        await _eventService.GetEvents();

    [HttpGet("getEvent/{Id}")]
    public async Task<ActionResult<Event>> GetEventById(int Id)
    {
        var _event = await _eventService.GetEventById(Id);

        if (_event is null)
        {
            return NotFound();
        }

        return _event;
    }

    [HttpGet("getEventByTypeOfEvent/{Id}")]
    public async Task<List<Event>> GetEventByTypeOfEvent(int Id) =>
        await _eventService.GetEventByTypeOfEvent(Id);

    [HttpGet("getEventByTag/{Id}")]
    public async Task<List<Event>> GetEventByTag(int Id) =>
        await _eventService.GetEventByTag(Id);

    [HttpPost("CreateEvent")]
    public async Task<IActionResult> CreateEvent(Event newEvent)
    {
        await _eventService.CreateEvent(newEvent);

        return CreatedAtAction(nameof(GetEventById), new { Id = newEvent.EventId }, newEvent);
    }

    [HttpPut("UpdateEvent/{Id}")]
    public async Task<IActionResult> UpdateEvent(int Id, Event updatedEvent) 
    {
        var _event = await _eventService.GetEventById(Id);

        if (_event is null)
        {
            return NotFound();
        }

        updatedEvent.EventId = _event.EventId;

        await _eventService.UpdateEvent(Id, updatedEvent);

        return NoContent();
    }

    [HttpDelete("DeleteEvent/{Id}")]
    public async Task<IActionResult> DeleteEvent(int Id)
    {
        var _event = await _eventService.GetEventById(Id);

        if (_event is null)
        {
            return NotFound();
        }

        await _eventService.DeleteEventById(Id);

        return NoContent();
    }

    }

}