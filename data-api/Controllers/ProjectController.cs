using Microsoft.AspNetCore.Mvc;
using data_api.Services;
using data_api.Models;

namespace data_api.Controllers;

[ApiController]
//[Route("api/[controller]")]
[Route("api/projects-collection")]
public class ProjectController : ControllerBase
{
    private readonly IDataService<Project> _service;

    public ProjectController(IDataService<Project> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetAll()
    {
        var projects = await _service.GetAllDataAsync();
        return Ok(projects); // produces status 200 ('ok')
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetById(string id)
    {
        var project = await _service.GetDataByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }

}