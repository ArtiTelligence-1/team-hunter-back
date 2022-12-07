using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Schemas
{
    public class Chat
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRequired]
        public long ChatId { get; set; }
        public int[]? ChatMessages { get; set; }
    }
}