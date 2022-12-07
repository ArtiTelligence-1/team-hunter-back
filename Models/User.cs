using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Models
{
    public class User
    {
        [BsonId]
        [BsonIgnoreIfNull]
        [BsonElement("_id")]
        public string? Id { get; set; }
        public long TelegramId { get; set; }
        public string? FirstName { get; set; }
        public string LastName { get; set; } = String.Empty;
        public string? PhotoUrl { get; set; }
        public string Bio { get; set; } = String.Empty;
        public string? AboutMe { get; set;}
        public string? Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Event> ActiveEvents { get; set; } = new List<Event>();
        public List<string> ActiveSessions { get; set; } = new List<string>();
    }
}