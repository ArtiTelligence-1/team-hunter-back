using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TeamHunter.Models;

public class Setting {
    [BsonId]
    [BsonIgnoreIfNull]
    [BsonElement("_id")]
    public ObjectId? Id { get; set; }
    [BsonRequired]
    public string? Key { get; set; }
    public IEnumerable<string> Value { get; set; } = new List<string>();
}