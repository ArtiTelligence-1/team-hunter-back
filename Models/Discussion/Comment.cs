using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamHunter.Models;

public class Comment : Reply
{
    public List<Comment> Replies { get;set; } = new List<Comment>();
}