using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Models;

public class UserShortInfo
{
    [BsonId]
    [BsonIgnoreIfNull]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string LastName { get; set; } = String.Empty;
    public string? PhotoUrl { get; set; }
}