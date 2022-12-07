using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Models
{
    public class Message
    {
        [BsonRequired]
        public User? Sender { get; set; }
        [BsonRequired]
        public string? Text { get; set; }
        public DateTime PostedAt { get; set; } = DateTime.Now;
        public DateTime? EditedAt { get; set; }
        public List<Message> Replies { get;set; } = new List<Message>();
    }
}