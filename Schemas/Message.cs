using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunterBackend.Schemas
{
    public class Message
    {
        [BsonId]
        [Key]
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement("_id")]
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