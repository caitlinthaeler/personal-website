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
    [BsonRepresentation(BsonType.DateTime)]
    public DateOnly? StartDate { get; set; } = null;

    [BsonElement("endDate")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateOnly? EndDate { get; set; } = null;

    [BsonElement("skills")]
    [BsonRepresentation(BsonType.Array)]
    public List<string> Skills { get; set; } = null!;

    [BsonElement("githubLink")]
    public string Githublink { get; set; } = null!;

    [BsonElement("hasLiveDemo")]
    [BsonRepresentation(BsonType.Boolean)]
    public bool HasLiveDemo { get; set; } = false;
    
    [BsonElement("media")]
    [BsonRepresentation(BsonType.Array)]
    public List<Media> Media { get; set; } = null!;

    /*
    data:
    ProjectName,
    ProjectDescription,
    SkillTags: {
        tagId1
        tagId2
        tagId3
    },
    ProjectStartDate
    ProjectEndDate
    ProjectThumbnail
    ProjectImages{
        img
        caption
    }
    InteractiveLink
    GithubLink
    */

}