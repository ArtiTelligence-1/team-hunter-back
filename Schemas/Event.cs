using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunterBackend.Schemas
{
    public class Event
    {
        [BsonId]
        [Key]
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement("_id")]
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