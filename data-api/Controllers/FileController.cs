using Microsoft.AspNetCore.Mvc;
using data_api.Services;
using System.Text.Json;

namespace data_api.Controllers;

[ApiController]
[Route("api")]
public class FileController : ControllerBase
{
    private readonly GitHubService _gitHubService;
    private readonly ILogger<FileController> _logger; // Add a logger field

    // Inject ILogger and GitHubService in the constructor
    public FileController(GitHubService gitHubService, ILogger<FileController> logger)
    {
        _gitHubService = gitHubService;
        _logger = logger;  // Initialize the logger
    }

    [HttpGet("")]
    public IActionResult GetApiInfo()
    {
        return Ok(new { message = "API is working" });
    }

    [HttpGet("image/{*filePath}")]
    public async Task<IActionResult> GetImageUrl(string filePath)
    {
        try
        {
            filePath = Uri.UnescapeDataString(filePath);
            _logger.LogInformation("Requested file path: {FilePath}", filePath); // Log information
            // Assuming GetFileAsync returns an object with file metadata, including the URL
            var imageResult = await _gitHubService.GetFileFromGitHub(filePath);
            
             if (imageResult is FileContentResult fileContentResult)
            {
                _logger.LogInformation("Image found: {FilePath}", filePath);
                return fileContentResult; // Directly return the file content
            }
            _logger.LogWarning("Image not found for path: {FilePath}", filePath); // Log a warning
            return NotFound(new { error = "Image not found" } );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while processing image request."); // Log an error with exception
            return BadRequest(new { error = ex.Message } );
        }
    }

    [HttpGet("json/{*fileName}")]
    public async Task<IActionResult> GetJsonFile(string fileName)
    {
        try
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "resources", "json", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(new { error = "JSON file not found"} );
            }

            var jsonContent = await System.IO.File.ReadAllTextAsync(filePath);
            return Content(jsonContent, "application/json");
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

}