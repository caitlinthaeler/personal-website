using System.Collections.Generic;
using System.Threading.Tasks;
using data_api.Services;

namespace data_api.Services;

public class UpdateService
{
    private readonly MongoDBService _mongoDBService;
    private readonly JsonFileService _jsonFileService;

    public UpdateService(MongoDBService mongoDBService, GitHubService gitHubService, JsonFileService jsonFileService)
    {
        _mongoDBService = mongoDBService;
        _jsonFileService = jsonFileService;
    }

    public async Task UpdateDataAsync()
    {
        // Get projects and skills from MongoDB
        var projects = await _mongoDBService.GetProjectsAsync();
        var skills = await _mongoDBService.GetSkillsAsync();

        // Update JSON files
        await _jsonFileService.UpdateProjects(projects);
        await _jsonFileService.UpdateSkills(skills);
    }
}
