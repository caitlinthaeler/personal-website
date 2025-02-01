using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using data_api.Models;
using MongoDB.Bson;
using System.IO;
using System.Reflection;


public class JsonFileService
{
    private readonly string projectsFilePath = "wwwroot/data/projects.json";
    private readonly string skillsFilePath = "wwwroot/data/skills.json";


    public async Task UpdateJsonFile<T>(string filePath, T data, string jsonKey)
    {
        //convert the dictionary to json using newtonsoft
        var dictionary = new Dictionary<string, object>();
        // Check if data is a collection
        if (data is IEnumerable<object> collection)
        {
            
            foreach (var item in collection)
            {
                // Use reflection to get the value of the property with the name of jsonKey
                var propertyInfo = item.GetType().GetProperty(jsonKey, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    // Get the value of the property
                    var keyValue = propertyInfo.GetValue(item)?.ToString() ?? string.Empty;
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        dictionary[keyValue] = item;
                    }
                }
                else
                {
                    // Handle case where property with jsonKey doesn't exist
                    throw new ArgumentException($"Property '{jsonKey}' not found in type {item.GetType().Name}");
                }
            }
        }
        else
        {
            throw new ArgumentException("Data is not a collection of objects.");
        }
        string jsonData = JsonSerializer.Serialize(dictionary, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, jsonData);
        }

    public async Task UpdateProjects(List<Project> projects)
    {
        await UpdateJsonFile(projectsFilePath, projects, "title");
    }
    public async Task UpdateSkills(List<Skill> skills)
    {
        await UpdateJsonFile(skillsFilePath, skills, "name");
    }


}
