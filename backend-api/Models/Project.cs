using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonalWebsiteApi.Models;

public class Project
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set;}

    [BsonElement("Name")]
    public string ProjectName { get; set;} = null!;

    [BsonElement("Description")]
    public string ProjectDescription { get; set; } = null!;

    [BsonElement("StartDate")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? ProjectStartDate { get; set; } = null;

    [BsonElement("EndDate")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? ProjectEndDate { get; set; } = null;

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