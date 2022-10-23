using TeamHunterBackend.Schemas;

namespace TeamHunterBackend.Services
{
    public interface IEventService
    {
        public  Task<List<Event>> GetEvents();
        public  Task<Event> GetEventById(int Id);
        public  Task<List<Event>> GetEventByTypeOfEvent(int Id);
        public  Task<List<Event>> GetEventByTag(int Id);
        public  Task CreateEvent(Event newEvent);
        public  Task UpdateEvent(int Id, Event updateEvent);
        public  Task DeleteEventById(int Id);

    }
}