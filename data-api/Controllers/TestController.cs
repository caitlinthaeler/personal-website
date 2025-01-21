using data_api.Services;
using data_api.Models;
using Microsoft.AspNetCore.Mvc;


namespace data_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly TestService _service;

    public TestController(TestService service)
    {
        _service = service;
    }

    //example
    [HttpGet("/test-create-project")]
    public Project TestCreateProject()
    {
        //await Create(project); for external call
        //await _service.CreateDataAsync(project); //internal call
        var dummyData = _service.GetAllData();
        return dummyData;
    }

    
}