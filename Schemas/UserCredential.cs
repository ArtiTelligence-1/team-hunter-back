using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunterBackend.Schemas
{
    public class UserCredential
    {
        [BsonId]
        [Key]
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement("_id")]
        public int CredId { get; set; }

        [BsonElement("user_id")]
        public string? UserId { get; set; }

        [BsonElement("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [BsonElement("password")]
        public string? Password { get; set; }
    }
}