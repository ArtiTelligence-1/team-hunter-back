using TeamHunterBackend.Schemas;

namespace TeamHunterBackend.Services
{
    public interface IEventMessageService
    {
        public  Task<List<Message>> GetMessages();
        public  Task<Message> GetMessageById(int Id);
        public  Task CreateMessage(Message newMessage);
        public  Task UpdateMessage(int Id, Message updateMessage); 
        public  Task DeleteMessageById(int Id);
    }
}