using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunterBackend.Schemas
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement("_id")]
        [BsonRequired]
        public int MessageId { get; set; }

        [BsonElement("ownerId")]
        public int OwnerId { get; set; }

        [BsonElement("message")]
        public string? Value { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("messageTime")]
        public DateTime MessageTime { get; set; }
    }
}