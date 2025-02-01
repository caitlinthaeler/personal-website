namespace data_api.Models;
public class Image
{
    
    public string Type { get; set; } = null!;
    public int Size { get; set; } = 0;
    public string Name { get; set; } = null!;
    public string Download_url { get; set; } = null!;
}