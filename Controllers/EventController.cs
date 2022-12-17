using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TeamHunter.Models;
using TeamHunter.Models.DTO;
using TeamHunter.Services;
using TeamHunter.Interfaces;
using System.Web;
namespace TeamHunter.Controllers ;
[ApiController]
[Route("/api/v1/events")]
[EnableCors("Policy")]
//[DisableCors]
public class EventsController : ControllerBase
{
    private IEventService _eventManager;
    private ISettingsService settings;

    public EventsController(ISettingsService settings, IEventService eventManager) {
        this.settings = settings;
        this._eventManager = eventManager;
    }

    [HttpGet("")]
    public async Task<List<Event>> GetAllEvents(string? eventType) {
        var events = await _eventManager.GetEventsAsync();
        
        Console.WriteLine(settings.EventTypes.Count());
        Console.WriteLine(settings.MessageRepliesLimit);

        if (eventType is null)
            return await _eventManager.GetEventsAsync();
        else if (eventType is not null)
            return await _eventManager.GetEventsByTypeAsync(eventType);

        return await _eventManager.GetEventsAsync(e => e.Type == eventType) ?? new List<Event>();
    }

    [HttpPost("")]
    public async Task<Event> CreateEvent(EventCreate newEvent)
    {
        // try:
            Event createdEvent = await _eventManager.CreateEventAsync(new User(), newEvent);
        // catch

        return createdEvent;
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<Event>> GetEventById(string Id)
    {
        var _event = await _eventManager.GetEventByIdAsync(Id);

        if (_event is null)
            return NotFound();

        return await _eventManager.GetEventByIdAsync(Id);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateEvent(string Id, EventUpdate updatedEvent) 
    {
        var _event = await _eventManager.GetEventByIdAsync(Id);

        if (_event is null)
        {
            return NotFound();
        }

        await _eventManager.ModifyEventAsync(Id, updatedEvent);

        return NoContent();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteEvent(string Id)
    {
        var _event = await _eventManager.GetEventByIdAsync(Id);

        if (_event is null)
        {
            return NotFound();
        }

        await _eventManager.DeleteEventAsync(Id);

        return NoContent();
    }
}