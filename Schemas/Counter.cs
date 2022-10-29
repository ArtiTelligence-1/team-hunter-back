using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunterBackend.Schemas
{
    public class Counter{
        [BsonId]
        [Key]
        [BsonRepresentation(BsonType.String)]
        [BsonElement("_id")]
        public string CounterName { get; set; } = string.Empty;
        [BsonElement("count")]
        public int Count { get; set; }
    }
}