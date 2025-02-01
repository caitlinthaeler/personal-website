using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace data_api.Models;
public class Image
{
    [JsonPropertyName("filePath")]
    [BsonElement("filePath")]
    public string FilePath { get; set; } = null!;
    [JsonPropertyName("size")]
    [BsonElement("size")]
    public int Size { get; set; } = 0;
    [JsonPropertyName("fileType")]
    [BsonElement("fileType")]
    public string FileType { get; set; } = null!;
}