using Microsoft.Extensions.Configuration;
using System;
using Octokit;
using System.Threading.Tasks;

namespace data_api.Services;

public class GitHubService
{
    private readonly GitHubClient _client;
    private readonly string _baseUrl;
    private readonly string _token;
    private readonly string _owner;
    public GitHubService(IConfiguration config)
    {
        var token = config["GitHub:AccessToken"];

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
}