using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace data_api.Models;
public class Skill
{
    public string Id { get; set; } = null!;

    [BsonElement("name")]
    public string Name { get; set; } = null!;
    public string[] Categories { get; set; } = null!;
}