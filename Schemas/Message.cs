using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Schemas
{
    public class Message
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRequired]
        public long MessageId { get; set; }
        public int OwnerId { get; set; }
        [BsonElement("message")]
        public string? Value { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime MessageTime { get; set; }
    }
}