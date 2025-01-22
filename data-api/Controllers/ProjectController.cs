using Microsoft.AspNetCore.Mvc;
using data_api.Services;
using data_api.Models;

namespace data_api.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [HttpPost]
    public async Task<ActionResult<Project>> Create([FromBody] Project project)
    {
        await _service.CreateDataAsync(project);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project); // returns status 201 ('created response')
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Project project)
    {
        var existingData = await _service.GetDataByIdAsync(id);
        if (existingData == null)
        {
            return NotFound();
        }
        await _service.UpdateDataAsync(id, project);
        return NoContent(); // returns status 204 ('no content')
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existingData = await _service.GetDataByIdAsync(id);
        if (existingData == null)
        {
            return NotFound();
        }
        await _service.DeleteDataAsync(id);
        return NoContent();
    }


}