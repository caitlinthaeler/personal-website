using Microsoft.Extensions.Configuration;
using System;
using Octokit;
using System.Threading.Tasks;
using dotenv.net;

namespace data_api.Services;

public class GitHubService
{
    private readonly GitHubClient _client;
    private readonly string _baseUrl;
    private readonly string _owner;
    private readonly string _repo;
    public GitHubService(IConfiguration config)
    {
         //Get the parent directory
        //  string currentDirectory = Directory.GetCurrentDirectory();
        //  string parentDirectory = Directory.GetParent(currentDirectory)?.FullName;

        // if (string.IsNullOrEmpty(parentDirectory))
        // {
        //     throw new Exception("Failed to locate the parent directory.");
        // }

        //  string envFilePath = Path.Combine(parentDirectory, ".env");

        // Console.WriteLine($"Resolved .env path: {envFilePath}");

        // if (!File.Exists(envFilePath))
        // {
        //     throw new FileNotFoundException(".env file not found at: " + envFilePath);
        // }
        // string envContent = File.ReadAllText(envFilePath);
        // Console.WriteLine("Contents of .env file:");
        // Console.WriteLine(envContent);

        // // Load the .env file
        // DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { envFilePath }));

        // // Read environment variables
        // var enVars = DotEnv.Read();

        // // Print out all key-value pairs
        // Console.WriteLine("Environment Variables Loaded:");
        // foreach (var kvp in enVars)
        // {
        //     Console.WriteLine($"{kvp.Key} = {kvp.Value}");
        // }

        //var token = enVars.ContainsKey("GITHUB_TOKEN") ? enVars["GITHUB_TOKEN"] : null;

        var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
        Console.WriteLine("GitHub Token Loaded: " + (!string.IsNullOrEmpty(token) ? "Yes" : "No"));

        if (string.IsNullOrEmpty(token))
        {
            throw new InvalidOperationException("GitHub AccessToken is not configured");
        }
        _client = new GitHubClient(new ProductHeaderValue("data-api"));
        _client.Credentials = new Credentials(token);
        _owner = config["GitHub:Owner"];
        _repo = config["GitHub:RepositoryName"];
    }

    public async Task<string> GetFileAsync(string owner, string repo, string filePath, string branch = "main")
    {
        try
        {
            var file = await _client.Repository.Content.GetAllContentsByRef(owner, repo, filePath, branch);
            return file[0].Content; // Decode base64 content if needed
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error fetching file contents", ex);
        }
    }

    public async Task<string> GetFileAsync(string filePath, string branch = "main")
    {
        try
        {
            var file = await _client.Repository.Content.GetAllContentsByRef(_owner, _repo, filePath, branch);
            return file[0].Content; // Decode base64 content if needed
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error fetching file contents", ex);
        }
    }

    public async Task<byte[]> GetFileRawContent(string owner, string repo, string filePath, string branch = "main")
    {
        try
        {
            Console.WriteLine($"Fetching: owner={owner}, repo={repo}, path={filePath}, branch={branch}");
            var fileContent = await _client.Repository.Content.GetRawContentByRef(owner, repo, filePath, branch);
            return fileContent;
        }
        catch (NotFoundException ex)
        {
            throw new Exception($"File not found. Check filePath, owner, and repo. {ex.Message}", ex);
        }
        catch (AuthorizationException ex)
        {
            throw new Exception($"Access denied. Check PAT and repository permissions. {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching file contents: {ex.Message}", ex);
        }
    }

    public async Task<string> GetImageUrlAsync(string owner, string repo, string filePath)
    {
        try
        {
            var file = await _client.Repository.Content.GetAllContentsByRef(owner, repo, filePath, "main");
            return file[0].DownloadUrl; // Decode base64 content if needed
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error fetching file contents", ex);
        }
    }

}