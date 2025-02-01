using Microsoft.AspNetCore.Mvc;
using data_api.Services;
using System.Text.Json;

namespace data_api.Controllers;

[ApiController]
[Route("api/{owner}/{repo}")]
public class FileController : ControllerBase
{
    private readonly GitHubService _gitHubService;

    public FileController(GitHubService gitHubService)
    {
        _gitHubService = gitHubService;
    }

    // [HttpGet("content/{*filePath}")]
    // public async Task<IActionResult> GetFileContent(string owner, string repo, string filePath)
    // {
    //     try
    //     {
    //         var content = await _gitHubService.GetFileAsync(owner, repo, filePath);
            
    //         return Ok(new  { content });
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest(new {error = ex.Message});
    //     }
    // }

    [HttpGet("image/{*filePath}")]
    public async Task<IActionResult> GetImageUrl(string owner, string repo, string filePath)
    {
        try
        {
            // Assuming GetFileAsync returns an object with file metadata, including the URL
            var imageUrl = await _gitHubService.GetImageUrlAsync(owner, repo, filePath);
            
            if (string.IsNullOrEmpty(imageUrl))
            {
                return NotFound(new { error = "Image URL not found" });
            }

            var result = new { imageUrl };
            //Console.WriteLine(result);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }


    // [HttpGet("file/{*filePath}")]
    // public async Task<IActionResult> GetFile(string owner, string repo, string filePath)
    // {
    //     try
    //     {
    //         var result = new Dictionary<string, object>();

    //         var content = await _gitHubService.GetFileAsync(owner, repo, filePath);
    //         result["content"] = content;

    //         var imageUrl = await _gitHubService.GetImageUrlAsync(owner, repo, filePath);
    //         if (!string.IsNullOrEmpty(imageUrl))
    //         {
    //             result["imageUrl"] = imageUrl;
    //         }   

    //         return Ok(result);
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest(new { error = ex.Message });
    //     }
    // }

}