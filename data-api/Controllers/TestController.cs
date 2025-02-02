using Microsoft.AspNetCore.Mvc;
using data_api.Services;

namespace data_api.Controllers;

[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult GetData()
    {
        Console.WriteLine("Test controller accessed");
        return Ok(new { message = "this is a test response. api is working!" });
    }
}