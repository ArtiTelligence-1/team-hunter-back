using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TeamHunter.Schemas;
using TeamHunter.Services;

namespace TeamHunter.Controllers 
{
    [ApiController]
    [Route("events")]
    [EnableCors("Policy")]
    //[DisableCors]
    public class EventsController : ControllerBase
    {
        private readonly MessageService _eventService;

    public EventsController(MessageService eventService) =>
        _eventService = eventService;

    [HttpGet("")]
    public async Task<List<Event>> FilterEvents(string? eventTag, EventType? eventType) {
        if (eventType is null && eventTag is null)
            return await _eventService.GetEvents();
        else if (eventType is null)
            return await _eventService.GetEventByTag(eventTag!);
        else if (eventTag is null)
            return await _eventService.GetEventByType(eventType ?? 0);

        return await _eventService.GetEvents(e => e.Tags.Any(tag => tag.Name == eventTag!) && e.Type == eventType) ?? new List<Event>();
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateEvent(Event newEvent)
    {
        await _eventService.CreateEvent(newEvent);

        return CreatedAtAction(nameof(GetEventById), new { Id = newEvent.Id }, newEvent);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<Event>> GetEventById(int Id)
    {
        var _event = await _eventService.GetEventById(Id);

        if (_event is null)
            return NotFound();

        return await _eventService.GetEventById(Id);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateEvent(int Id, Event updatedEvent) 
    {
        var _event = await _eventService.GetEventById(Id);

        if (_event is null)
        {
            return NotFound();
        }

        updatedEvent.Id = _event.Id;

        await _eventService.UpdateEvent(Id, updatedEvent);

        return NoContent();
    }

    [HttpDelete("{Id}")]
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