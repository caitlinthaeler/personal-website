using System.Net.Http.Headers;
using System.IO;

namespace data_api.Services;

public class GitHubService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _token;
    private readonly string _owner;
    public GitHubService(IConfiguration config)
    {
        _baseUrl = config["GitHub:BaseUrl"];
        _token = config["GitHub:PersonalAccessToken"];
        _owner = config["GitHub:Username"];
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _token);
    }

    public async Task<(Stream Content, string ContentType)> GetFileAsync(string repo, string branch, string filePath)
    {
        var url = $"{_baseUrl}/repos/{_owner}/{repo}/contents/{branch}/{filePath}?ref={branch}";
        Console.WriteLine(url);
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to fetch file: {response.ReasonPhrase}");

        var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";
        var contentStream = await response.Content.ReadAsStreamAsync();
        return (contentStream, contentType);
    }
}