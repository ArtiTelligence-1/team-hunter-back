using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunterBackend.Schemas
{
    public class User
    {
        [BsonId]
        [Key]
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement("_id")]
        public int UserId { get; set; }
//add USERNAME
        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("surname")]
        public string? Surname { get; set; }

        [BsonElement("sex")]
        public string? Sex { get; set; }

        [BsonElement("age")]
        public int Age { get; set; }

        // [BsonElement("phoneNumber")]
        // public string? PhoneNumber { get; set; }

        [BsonElement("tags")]
        public List<int>? Tags { get; set; } 

        [BsonElement("aboutMe")]
        public string? AboutMe { get; set;}

        [BsonElement("photo")]
        public int Photo { get; set; }

        [BsonElement("events")]
        public List<int>? Events { get; set; } 
    }
}