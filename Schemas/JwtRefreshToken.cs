using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunterBackend.Schemas
{
    public class JwtRefreshToken
    {
        [BsonId]
        [Key]
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement("_id")]
	    public int TokenId { get; set; }

        [BsonElement("user_id")]
	    public string? UserId { get; set; }
        
        [BsonElement("refreshToken")]
	    public string? RefreshToken { get; set; }
    }
}