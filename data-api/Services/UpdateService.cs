using System.Collections.Generic;
using System.Threading.Tasks;
using data_api.Services;

namespace data_api.Services;

public class UpdateService
{
    private readonly MongoDBService _mongoDBService;
    private readonly JsonFileService _jsonFileService;
    private readonly GitHubService _gitHubService;

    public UpdateService(MongoDBService mongoDBService, GitHubService gitHubService, JsonFileService jsonFileService)
    {
        _mongoDBService = mongoDBService;
        _jsonFileService = jsonFileService;
        _gitHubService = gitHubService;
    }

    public async Task UpdateDataAsync()
    {
        // Get projects and skills from MongoDB
        var projects = await _mongoDBService.GetProjectsAsync();
        foreach (var project in projects)
        {
            if (project.Thumbnail != null && !string.IsNullOrEmpty(project.Thumbnail.FilePath))
            {
                var result = await _gitHubService.GetImageMetadataAsync(project.Thumbnail.FilePath);
                if (result != null)
                {
                    project.Thumbnail = result;
                }
            }
        }
        var skills = await _mongoDBService.GetSkillsAsync();

        // Update JSON files
        await _jsonFileService.UpdateProjects(projects);
        await _jsonFileService.UpdateSkills(skills);
    }
}
