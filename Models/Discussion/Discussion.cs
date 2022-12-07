using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Models;

public class Discussion {
    [BsonId]
    [BsonRequired]
    public string? EventId { get; set; }
    public List<Comment> Messages { get; set; } = new List<Comment>();
}