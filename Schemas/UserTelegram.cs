using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunterBackend.Schemas
{
    public class UserTelegram
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? UserName { get; set; }
        public string? Photo { get; set; }
        public int AuthDate { get; set; }
        public string? Hash { get; set; }
    }
}