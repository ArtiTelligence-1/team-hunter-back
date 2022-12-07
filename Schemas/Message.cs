using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Schemas
{
    public class Message
    {
        [BsonRequired]
        public User? Owner { get; set; }
        public Message? ReplyTo { get; set;}
        public string? Text { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; }
        public DateTime? Edit { get; set; }
    }
}