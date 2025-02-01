using MongoDB.Driver;
using data_api.Models;
using data_api.Services;

namespace data_api.Services;
public class MongoDBService
{
    private readonly IMongoCollection<Project> _projectsCollection;
    private readonly IMongoCollection<Skill> _skillsCollection;

    public MongoDBService(string MongoConnectionKey, string databaseName)
    {
        //connect to mongodb
        var connectionString = Environment.GetEnvironmentVariable(MongoConnectionKey);
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

    public async Task<List<Project>> GetProjectsAsync() => await _projectsCollection.Find(_ => true).ToListAsync();

    public async Task<List<Skill>> GetSkillsAsync() => await _skillsCollection.Find(_ => true).ToListAsync();
}

