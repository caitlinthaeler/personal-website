using data_api.Models;
using System.Threading.Tasks;



namespace data_api.Services;
public class TestDataService<T> : IDataService<T> where T : class
{
    private static readonly Project project = new Project
        {
            Id = "0",
            Title = "Rogue Cat",
            Description = "An immersive Warriors inspired game combining 3d and 3d graphics for stunning visuals. Simulate the daily life of a Clan cat and get to know your fellow clan members and their life stories",
            Skills = { "C#", "MongoDB", "REST API" },
            // Media =
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

    public Task<List<T>> GetAllDataAsync()
    {
        var dummyData = new List<T>()
        {
            project as T
            
        };
        return Task.FromResult(dummyData);
    }
    public Task<T> GetDataByIdAsync(string id)
    {
        // If T is Project, return the project as T
        if (typeof(T) == typeof(Project))
        {
            var dummyData = project as T;
            return Task.FromResult(dummyData);
        }

        // If T isn't Project, return null
        return Task.FromResult<T>(null);
    }
    public Task CreateDataAsync(T data)
    {
        var dummyData = project as T;
        return Task.FromResult(dummyData);
    }
    public Task UpdateDataAsync(string id, T data)
    {
        var dummyData = data as T;
        return Task.FromResult(dummyData);
    }
    public Task DeleteDataAsync(string id)
    {
        var dummyData = project as T;
        return Task.FromResult(dummyData);
    }
}