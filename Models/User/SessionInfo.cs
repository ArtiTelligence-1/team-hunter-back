using System.Net;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Models;

public class SessionInfo {
    [BsonId]
    [BsonElement("_id")]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    [BsonRequired]
    public string? IPAddress { get; set; }
    public string? Agent { get; set; }
    public DateTime StartTime { get; set; } = DateTime.Now;
}