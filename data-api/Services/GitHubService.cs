using Octokit;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using data_api.Models;
using Microsoft.AspNetCore.StaticFiles;

namespace data_api.Services;

public class GitHubService
{
    private readonly GitHubClient _client;
    private readonly string _owner;
    private readonly string _repo;
    private readonly ConcurrentDictionary<string, byte[]> _cache;

    public GitHubService(string clientName, string token, string owner, string repo)
    {
        //var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
        //Console.WriteLine("GitHub Token Loaded: " + (!string.IsNullOrEmpty(token) ? "Yes" : "No"));
        if (string.IsNullOrEmpty(token))
        {
            throw new InvalidOperationException("GitHub AccessToken is not configured");
        }
        _client = new GitHubClient(new ProductHeaderValue(clientName));
        _client.Credentials = new Credentials(token);
        _owner = owner;
        _repo = repo;
        _cache = new ConcurrentDictionary<string, byte[]>();
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
            //Console.WriteLine($"Fetching: owner={owner}, repo={repo}, path={filePath}, branch={branch}");
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

    public async Task<Image?> GetImageMetadataAsync(string filePath)
    {
        try
        {
            var file = await _client.Repository.Content.GetAllContentsByRef(_owner, _repo, filePath, "main");
            if (file is { Count: > 0})
            {
                var content = file[0];

                return new Image
                {
                    FilePath = filePath,
                    Name = content.Name,
                    Size = content.Size,
                    FileType = GetMimeType(content.Name),
                };
            }
        }
        catch (NotFoundException)
        {
            return null;
        }
        return null;
    }

    public async Task<IReadOnlyList<RepositoryContent>> GetImage(string owner, string repo, string filePath, string branch="main")
    {
        try
        {
            var fileData = await _client.Repository.Content.GetAllContentsByRef(owner, repo, filePath, branch);
            return fileData; // Decode base64 content if needed
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error fetching file contents", ex);
        }
    }

    public async Task<IActionResult> GetFileFromGitHub(string filePath)
    {
        string cacheKey = $"{_repo}/{filePath}";
        if (_cache.ContainsKey(cacheKey)){
            return new FileContentResult(_cache[cacheKey],"image/png");
        }
        try
        {
            //check if the path is available
            var file = await _client.Repository.Content.GetAllContentsByRef(_owner, _repo, filePath, "main");

            // If file is found, fetch the content
            if (file != null && file.Count > 0)
            {
                var content = file[0];
                var downloadUrl = content.DownloadUrl;
                var fileContent = await _client.Repository.Content.GetRawContentByRef(_owner, _repo, filePath, "main");

                _cache[cacheKey] = fileContent;

                return new FileContentResult(fileContent, GetMimeType(filePath));
            }
            else
            {
                return new NotFoundResult();
            }
        }
        catch (Exception)
        {
            return new NotFoundResult();
        }
        

    }

    

    public string GetMimeType(string fileName)
    {
        var provider = new FileExtensionContentTypeProvider();
        return provider.TryGetContentType(fileName, out var mimeType) ? mimeType : "application/octet-stream";
    }


}