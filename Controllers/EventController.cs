using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TeamHunter.Schemas;
using TeamHunter.Services;

namespace TeamHunter.Controllers 
{
    [ApiController]
    [Route("/api/v1/events")]
    [EnableCors("Policy")]
    //[DisableCors]
    public class EventsController : ControllerBase
    {
        private readonly EventService _eventService;

    public EventsController(EventService eventService) =>
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
    public async Task<Event> CreateEvent(EventUpdate newEvent)
    {
        // try:
            Event createdEvent = await _eventService.CreateEvent(newEvent);
        // catch

        return createdEvent;
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<Event>> GetEventById(string Id)
    {
        var _event = await _eventService.GetEventById(Id);

        if (_event is null)
            return NotFound();

        return await _eventService.GetEventById(Id);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateEvent(string Id, EventUpdate updatedEvent) 
    {
        var _event = await _eventService.GetEventById(Id);

        if (_event is null)
        {
            return NotFound();
        }

        await _eventService.UpdateEvent(Id, updatedEvent);

        return NoContent();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteEvent(string Id)
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