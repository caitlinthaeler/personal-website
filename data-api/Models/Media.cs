namespace data_api.Models;
public class Media
{
    public string Id { get; set; } = null!;
    public string Type { get; set; } = null!; //image, video, game, icon etc
    public string Url { get; set; } = null!;
    public string DisplayText { get; set; } = null!;
    public string IconUrl { get; set; } = null!;
    public Dictionary<string, string>? Metadata { get; set; } // Optional custom properties
}