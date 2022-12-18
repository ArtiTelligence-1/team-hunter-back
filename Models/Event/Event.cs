using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Models;

public class Event
{
    [BsonId]
    [BsonElement("_id")]
    public string Id { get; set; } = Convert.ToString(DateTime.Now.Ticks / 10000, 16);
    [BsonRequired]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Owner { get; set; }
    [BsonRequired]
    public string? Title { get; set; }
    [BsonRequired]
    public string? Type { get; set; }
    [BsonRequired]
    public int ParticipantsLimit { get; set; }
    public List<UserShortInfo> Participants { get; set; } = new List<UserShortInfo>();
    [BsonRequired]
    public AgeInterval? AgeLimitGap { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime HoldingTime { get; set; } = new DateTime((DateTime.Now + new TimeSpan(3,0,0,0)).Ticks);
    [BsonRequired]
    public Location? Location { get; set; }
    [BsonRequired]
    public string? Description { get; set; }
    public string? PosterUrl { get; set; }
    /// <summary>Up To 10 messages in event chat</summary>
    public List<Comment> Discussion { get; set; } = new List<Comment>();
}