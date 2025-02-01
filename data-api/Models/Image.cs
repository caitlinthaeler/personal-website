using System.Text.Json.Serialization;

namespace data_api.Models;
public class Image
{

    [JsonPropertyName("name")]
    public string FilePath { get; set; } = string.Empty!;
    [JsonPropertyName("size")]
    public int Size { get; set; } = 0;
    [JsonPropertyName("fileType")]
    public string FileType { get; set; } = null!;
}