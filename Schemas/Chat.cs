using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunterBackend.Schemas
{
    public class Chat
    {
        [BsonId]
        [Key]
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement("_id")]
        public int ChatId { get; set; }

        [BsonElement("chatMessages")]
        public int[]? ChatMessages { get; set; }
    }
}