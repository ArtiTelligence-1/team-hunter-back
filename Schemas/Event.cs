using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Schemas;
    
// "eventId" : "ObjectId",
// "title*" : "String",
// "kindOfSport*" : "kindOfSportId",
// "numOfPeople*" : "Number",
// "ageInterval*" : "String('to18', '18to25', '25to', 'everybody')",
// "timeOfEvent*" : "String(DateTime)",
// "location*" : "String",
// "description*" : "String",

// "currentNumOfPeople" : "Number"

public class Event
{
    [BsonId]
    [BsonElement("_id")]
    // [BsonRequired]
    public string Id { get; set; } = Convert.ToString(DateTime.Now.Ticks / 10000, 16);
    [BsonRequired]
    public string Title { get; set; } = String.Empty;
    public EventType Type { get; set; }
    public List<EventTag> Tags { get; set; } = new List<EventTag>();
    public AgeInterval? AgeInterval { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime HoldingTime { get; set; }
    public Location? Location { get; set; }
    public string? Description { get; set; }
    public int ParticipantLimit { get; set; }
    public List<User> Participants { get; set; } = new List<User>();
    public List<Message> Messages { get; set; } = new List<Message>();
}