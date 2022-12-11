using System.Net;
using MongoDB.Bson;

namespace TeamHunter.Models;

public class SessionInfo {
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    public IPAddress? IPAddress { get; set; }
    public string? Agent { get; set; }
    public DateTime StartTime { get; set; } = DateTime.Now;
}