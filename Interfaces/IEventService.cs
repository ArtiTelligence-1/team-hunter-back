using TeamHunter.Models;
using TeamHunter.Models.DTO;
using System.Linq.Expressions;

namespace TeamHunter.Interfaces;

public interface IEventService
{
    Task<List<Event>> GetEvents();
    Task<List<Event>> GetEvents(Expression<Func<Event, bool>> filter);
    Task<Event> GetEventById(string eventId);
    Task<Event> GetEventByType(string type);
    Task<Event> CreateEvent(EventCreate eventCreate);
    Task<Event> DeleteEvent(string eventId);
    Task<Event> ModifyEvent(string eventId, EventUpdate eventUpdate);
    Task JoinEvent(string eventId, User participant);
    Task LeaveEvent(string eventId, User participant);
    Task<Reply> PostComment(string eventId, User participant, string text, DateTime? replyToCommentId);
    Task DeleteComment(string eventId, DateTime commentId);
    
}