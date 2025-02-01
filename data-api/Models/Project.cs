using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace data_api.Models;

public class Project
{
    //public string? Id { get; set;}
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("title")]
    public string Title { get; set;} = null!;

    [BsonElement("description")]
    public string Description { get; set; } = null!;

    [BsonElement("startDate")]
    public DateTime? StartDate { get; set; } = null;

    [BsonElement("endDate")]
    public DateTime? EndDate { get; set; } = null;

    [BsonElement("skills")]
    public List<string> Skills { get; set; } = new List<string>();

    [BsonElement("githubLink")]
    public string Githublink { get; set; } = null!;

    [BsonElement("hasLiveDemo")]
    [BsonRepresentation(BsonType.Boolean)]
    public bool HasLiveDemo { get; set; } = false;
    
    [BsonElement("media")]
    public List<string> Media { get; set; } = new List<string>();

    [BsonElement("thumbnail")]
    [BsonSerializer(typeof(ThumbnailSerializer))]
    public Image Thumbnail { get; set; } = null!;

}