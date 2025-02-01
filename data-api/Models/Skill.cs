using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace data_api.Models;
public class Skill
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    [BsonElement("description")]
    [JsonPropertyName("description")]
    public string Description{ get; set; } = null!;
    [BsonElement("categories")]
    [JsonPropertyName("categories")]
    public string[] Categories { get; set; } = null!;
}