using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace data_api.Models;

public class Project
{
    //[BsonId]
    //[BsonRepresentation(BsonType.ObjectId)]
    //public string? Id { get; set;}
    public string Id { get; set; } = null!;

    //[BsonElement("Name")]
    public string Title { get; set;} = null!;

    //[BsonElement("Description")]
    public string Description { get; set; } = null!;

    //[BsonElement("StartDate")]
    //[BsonRepresentation(BsonType.DateTime)]
    public DateOnly? StartDate { get; set; } = null;

    //[BsonElement("EndDate")]
    //[BsonRepresentation(BsonType.DateTime)]
    public DateOnly? EndDate { get; set; } = null;

    public List<string> Skills { get; set; } = null!;
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