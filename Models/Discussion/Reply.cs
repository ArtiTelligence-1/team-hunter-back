using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Models;
public class Reply
{
    [BsonRequired]
    public User? Sender { get; set; }
    [BsonRequired]
    public string? Text { get; set; }
    [BsonId]
    public DateTime PostedAt { get; set; } = DateTime.Now;
    public DateTime? EditedAt { get; set; }
}