// using Microsoft.Extensions.Options;
// using MongoDB.Driver;
// using TeamHunter.Interfaces;
// using TeamHunter.Models;
// using System.Linq.Expressions;

// namespace TeamHunter.Services;

// public class EventService
// {
//     private readonly IMongoCollection<Event> eventsCollection;

//     public EventService(IDBSessionManagerService sessionManager)
//     {
//         eventsCollection = sessionManager.GetCollection<Event>();
//     }

    
//     public async Task<Event> GetEventById(string Id) =>
//         await eventsCollection.Find(m => m.Id == Id).FirstOrDefaultAsync();
//     public async Task<List<Event>> GetEvents() => 
//         await eventsCollection.Find(_ => true).ToListAsync();
//     public async Task<List<Event>> GetEvents(Expression<Func<Event, bool>> filter) =>
//         await eventsCollection.Find(filter).ToListAsync();
//     public async Task<List<Event>> GetEventByType(string type) =>
//         await this.GetEvents(e => e.Type == type);
//     public async Task SendMessage(string eventId, Comment message) {
//         Event currentEvent = await this.GetEventById(eventId);
//         currentEvent.Discussion.Append(message);

//         await this.UpdateEvent(currentEvent.Id!, EventUpdate.fromEvent(currentEvent));
//     }

//     public async Task EditMessage(string eventId, DateTime indentifier, string new_message){
//         Event currentEvent = await this.GetEventById(eventId);
//         Comment msg = currentEvent.Discussion.Single(m => m.Date == indentifier);
//         msg.Text = new_message;
//         msg.Edit = DateTime.Now;

        
//     }

//     public async Task<Event> CreateEvent(EventUpdate createEventData) {
//         Event newEvent = createEventData.toEvent();
//         await eventsCollection.InsertOneAsync(newEvent);
//         return newEvent;
//     }

//     // 
//     public async Task UpdateEvent(string eventId, EventUpdate update) {
//         // await _events.ReplaceOneAsync(m => m.Id == Id, updateEvent);
//     }

//     public async Task DeleteEventById(string Id) =>
//         await eventsCollection.DeleteOneAsync(m => m.Id == Id);
// }
