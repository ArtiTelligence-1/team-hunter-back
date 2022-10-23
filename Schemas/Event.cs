using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunterBackend.Schemas
{
    
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
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement("_id")]
        [BsonRequired]
        public int EventId { get; set; }

        [BsonElement("title")]
        public string? Title { get; set; }

        [BsonElement("typeOfEvent")]
        public int TypeOfEvent { get; set; }

        [BsonElement("numOfPeople")]
        public int NumOfPeople { get; set; }

        [BsonElement("ageInterval")]
        public string? AgeInterval { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("timeOfEvent")]
        public DateTime TimeOfEvent { get; set; }

        [BsonElement("location")]
        public string? Location { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("tags")]
        public int[]? Tags { get; set; }

        [BsonElement("currentNumOfPeople")]
        public int CurrentNumOfPeople { get; set; }

        [BsonElement("chatId")]
        public int ChatId { get; set; }
    }
}