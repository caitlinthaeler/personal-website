using data_api.Models;

namespace data_api.Services;

public class TestService
{
    private static readonly Project project = new()
    {
        Id = "1",
        Title = "Rogue Cat",
        Description = "An immersive Warriors inspired game combining 3d and 3d graphics for stunning visuals. Simulate the daily life of a Clan cat and get to know your fellow clan members and their life stories",
        Skills = new() {"C#", "MongoDB", "REST API" },
        // Media = new List<Media>()
        // {
        //     new Media 
        //     {
        //         Type = "image", 
        //         Url = "https://c-sharp.com",
        //         Metadata = new Dictionary<string, string>()
        //         {
        //             {"skill", "C#"}
        //         },
        //     },
        // },

        StartDate = new DateTime(2019, 1, 1),
        EndDate = new DateTime(2021, 1, 1)
    };

    public Project GetAllData()
    {
        Console.WriteLine($"Project id: {project.Id}");
        return project;
    }

}