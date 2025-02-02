using Microsoft.AspNetCore.Mvc;
using data_api.Services;

namespace data_api.Controllers;

[ApiController]
[Route("api")]
public class TestController : ControllerBase
{
    [HttpGet("test")]
    public IActionResult GetData()
    {
        return Ok(new { message = "this is a test response. api is working!" });
    }
}