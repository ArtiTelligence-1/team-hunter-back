using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TeamHunter.Models;
using TeamHunter.Models.DTO;
using TeamHunter.Services;
using TeamHunter.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using System.IdentityModel.Tokens.Jwt;

namespace TeamHunter.Controllers ;
[ApiController]
[Route("/api/v1/events")]
[EnableCors("Policy")]
//[DisableCors]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventManager;
    private readonly ISettingsService _settings;
    private readonly IUserService _userManager;

    public EventsController(
            ISettingsService settings,
            IEventService eventManager,
            IUserService userManager) {
        this._settings = settings;
        this._eventManager = eventManager;
        this._userManager = userManager;
    }

    [HttpGet("")]
    public async Task<List<Event>> GetAllEvents(string? eventType) {
        var events = await _eventManager.GetEventsAsync();

        if (eventType is null)
            return await _eventManager.GetEventsAsync();
        else if (eventType is not null)
            return await _eventManager.GetEventsByTypeAsync(eventType);

        return await _eventManager.GetEventsAsync(e => e.Type == eventType) ?? new List<Event>();
    }

    [HttpPost("")]
    public async Task<Event> CreateEvent([FromBody] EventCreate newEvent)
    {
        try {
            return await _eventManager.CreateEventAsync(new User(), newEvent);
        } catch (System.ArgumentNullException){
            return new Event();
        }
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

    [HttpGet("{id}/comment")]
    public async Task<Discussion> GetComments(string id, int limit = 0) {
        // await _eventManager.LoadCommentsAsync(id);
        return await _eventManager.LoadCommentsAsync(id);
    }

    [HttpPost("{id}/comment")]
    [Authorize]
    public async Task<Comment> AddComment(string id, [FromBody]CommentCreate newComment) {
        User? user = await _userManager.GetUserFromSessionAsync(Request);
        return await _eventManager.PostCommentAsync(id, user!, newComment.text!);
    }

    [HttpPost("{id}/join")]
    public async Task<ResponseMessage> JoinEvent(string id){
        User? user = await _userManager.GetUserFromSessionAsync(Request);
        Event currentEvent = await _eventManager.GetEventByIdAsync(id);

        if (user is not null){
            if (currentEvent.Participants.Where(u => u.Id == user.Id).Count() > 0){
                await _eventManager.LeaveEventAsync(id, user!);
                return ResponseMessage.Ok("unjoined");
            }
            else {
                await _eventManager.JoinEventAsync(id, user!);
                return ResponseMessage.Ok("joined");
            }
        }

        return ResponseMessage.Message("user not found");
    }
}