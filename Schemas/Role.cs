using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunterBackend.Schemas
{
    public static class Role
    {
        public const string Admin = "admin";
        public const string User = "user"; 
    }
}