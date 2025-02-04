using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace data_api.Models;
public class Media
{
    [BsonElement("mediaId")]
    [JsonPropertyName("mediaId")]
    public string MediaId { get; set; } = null!;
    
    [BsonElement("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; } = null!; //image, video, game, icon etc
    public string Url { get; set; } = null!;
    public string DisplayText { get; set; } = null!;
    public string IconUrl { get; set; } = null!;
    public Dictionary<string, string>? Metadata { get; set; } // Optional custom properties
}