using MongoDB.Driver;
using data_api.Models;

namespace data_api.Services;
public class MongoDBService
{
    private readonly IMongoCollection<Project> _projectsCollection;
    private readonly IMongoCollection<Skill> _skillsCollection;

    public MongoDBService(string databaseName, string connectionString)
    {
        //connect to mongodb
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("MONGO_URI is not configured");
        }

        //connect to database
        if (string.IsNullOrEmpty(databaseName))
        {
            throw new InvalidOperationException("DatabaseName is not configured");
        }
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        
        //assign routes to collections
        var projectCollection = "projects";
        var skillsCollection = "skills";
        _projectsCollection = database.GetCollection<Project>(projectCollection);
        _skillsCollection = database.GetCollection<Skill>(skillsCollection);
    }

    public async Task<List<Project>> GetProjectsAsync(){
        var projects = await _projectsCollection.Find(_ => true).ToListAsync();
        foreach (var project in projects)
        {
            if (project.Thumbnail != null)
            {
                
            }
        }
        return projects;
    }

    public async Task<List<Skill>> GetSkillsAsync() => await _skillsCollection.Find(_ => true).ToListAsync();
}

