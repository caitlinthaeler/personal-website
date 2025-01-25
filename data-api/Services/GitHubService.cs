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
    public GitHubService(IConfiguration config)
    {
        DotEnv.Load();
        var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

        if (string.IsNullOrEmpty(token))
        {
            throw new InvalidOperationException("GitHub AccessToken is not configured");
        }
        _client = new GitHubClient(new ProductHeaderValue("data-api"));
        _client.Credentials = new Credentials(token);
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
}