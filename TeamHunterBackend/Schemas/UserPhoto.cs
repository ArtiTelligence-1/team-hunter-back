using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunterBackend.Schemas
{
    public class UserPhoto
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement("_id")]
        public int PhotoId { get; set; }

        [BsonRepresentation(BsonType.Binary)]
        [BsonElement("Photo")]
        public byte[]? Photo { get; set; }
    }
}