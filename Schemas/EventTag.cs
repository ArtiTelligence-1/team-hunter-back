using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Schemas;

public class EventTag
{
    [BsonId]
    [BsonElement("_id")]
    public int EventTagId { get; set; }

    [BsonElement("tagName")]
    public string? Name { get; set; }
}
