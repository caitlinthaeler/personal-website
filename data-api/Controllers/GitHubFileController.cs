using Microsoft.AspNetCore.Mvc;
using data_api.Services;


namespace data_api.Controllers;

[ApiController]
[Route("api/")]
public class FileController : ControllerBase
{
    private readonly GitHubService _gitHubService;

    public FileController(GitHubService gitHubService)
    {
        _gitHubService = gitHubService;
    }

    [HttpGet("{owner}/{repo}/{*filePath}")]
    public async Task<IActionResult> GetFile(string owner, string repo, string filePath)
    {
        try
        {
            var content = await _gitHubService.GetFileAsync(owner, repo, filePath);
            return Ok(new  { content });
        }
        catch (Exception ex)
        {
            return BadRequest(new {error = ex.Message});
        }
    }
}