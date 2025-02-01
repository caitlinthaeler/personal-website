using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using data_api.Models;

public class JsonFileService
{
    private readonly string projectsFilePath = "wwwroot/data/projects.json";

    public async Task UpdateJsonFile<T>(string filePath, T data)
    {
        string jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, jsonData);
    }

    public async Task UpdateProjects(List<Project> projects)
    {
        await UpdateJsonFile(projectsFilePath, projects);
    }
}
