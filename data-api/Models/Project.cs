using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace data_api.Models;

public class Project
{
    //public string? Id { get; set;}
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [BsonElement("title")]
    [JsonPropertyName("title")]
    public string Title { get; set;} = null!;

    [BsonElement("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;

    [BsonElement("startDate")]
    [JsonPropertyName("startDate")]
    public DateTime? StartDate { get; set; } = null;

    [BsonElement("endDate")]
    [JsonPropertyName("endDate")]
    public DateTime? EndDate { get; set; } = null;

    [BsonElement("skills")]
    [JsonPropertyName("skills")]
    public List<string> Skills { get; set; } = new List<string>();

    [BsonElement("githubLink")]
    [JsonPropertyName("githubLink")]
    public string Githublink { get; set; } = null!;

    [BsonElement("hasLiveDemo")]
    [JsonPropertyName("hasLiveDemo")]
    [BsonRepresentation(BsonType.Boolean)]
    public bool HasLiveDemo { get; set; } = false;
    
    [BsonElement("media")]
    [JsonPropertyName("media")]
    public List<string> Media { get; set; } = new List<string>();

    [BsonElement("thumbnail")]
    [JsonPropertyName("thumbnail")]
    public Image Thumbnail { get; set; } = null!;

}