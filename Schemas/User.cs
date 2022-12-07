using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Schemas
{
    public class User
    {
        //[System.ComponentModel.DataAnnotations.Required]
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement("_id")]
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Sex { get; set; }
        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
        public List<int>? Tags { get; set; } 
        public string? AboutMe { get; set;}
        public int Photo { get; set; }
        public List<int>? Events { get; set; } 
    }
}