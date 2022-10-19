using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunterBackend.Schemas
{
    public class Chat
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement("_id")]
        [BsonRequired]
        public int ChatId { get; set; }

        [BsonElement("chatMessages")]
        public int[]? ChatMessages { get; set; }
    }
}