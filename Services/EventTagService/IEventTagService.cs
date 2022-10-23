using TeamHunterBackend.Schemas;

namespace TeamHunterBackend.Services
{
    public interface IEventTagService
    {
        public  Task<List<EventTag>> GetEventTags();
        public  Task<EventTag> GetEventTagById(int Id);
        public  Task CreateEventTag(EventTag newEventTag);
        public  Task DeleteEventTagById(int Id);
    }
}