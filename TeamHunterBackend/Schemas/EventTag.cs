using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunterBackend.Schemas
{
    public class EventTag
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement("_id")]
        public int EventTagId { get; set; }

        [BsonElement("tagName")]
        public string? TagName { get; set; }
    }
}