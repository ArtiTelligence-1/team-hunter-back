using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Models;

public class Comment
{
    [BsonId]
    public DateTime Id { get; set; } = DateTime.Now;
    [BsonRequired]
    public UserShortInfo? Sender { get; set; }
    [BsonRequired]
    public string? Text { get; set; }
    public DateTime? EditedAt { get; set; }
    public DateTime? ReplyTo { get; set; }
    public List<DateTime> Replies { get; set; } = new List<DateTime>();
}