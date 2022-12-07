using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Models;

public class Options {
    [BsonId]
    [BsonIgnoreIfNull]
    [BsonElement("_id")]
    public string? Id { get; set; }
    [BsonRequired]
    public string? Key { get; set; }
    public List<string> Value { get; set; } = new List<string>();
}