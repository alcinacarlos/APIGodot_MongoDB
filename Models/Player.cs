namespace APIGodot.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class Player
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string? Name { get; set; }
    public int MaxScore { get; set; }
}