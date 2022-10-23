using TeamHunterBackend.Schemas;

namespace TeamHunterBackend.Services
{
    public interface IChatService
    {
         public  Task<List<Chat>> GetChats();
         public  Task<Chat> GetChatById(int Id);
         public  Task CreateChat(Chat newChat);
         public  Task UpdateChat(int Id, Chat updateChat);
         public  Task DeleteChatById(int Id);
    }
}