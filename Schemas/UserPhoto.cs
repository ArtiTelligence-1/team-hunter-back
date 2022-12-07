using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Schemas
{
    public class Photo
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement("_id")]
        public int Id { get; set; }

        [BsonRepresentation(BsonType.Binary)]
        public byte[]? Data { get; set; }
    }
}