using Microsoft.AspNetCore.Mvc;
using data_api.Services;


namespace data_api.Controllers;

[ApiController]
[Route("api/media")]
public class FileController : ControllerBase
{
    private readonly GitHubService _gitHubService;

    public FileController(GitHubService gitHubService)
    {
        _gitHubService = gitHubService;
    }

    [HttpGet("{repo}/{branch}/{*filePath}")]
    public async Task<IActionResult> GetFile(string repo, string branch, string filePath)
    {
        try
        {
            (Stream content, string contentType) = await _gitHubService.GetFileAsysnc(repo, branch, filePath);
            return File(content, contentType);
        }
        catch (Exception ex)
        {
            return BadRequest(new {error = ex.Message});
        }
    }
}