using Microsoft.AspNetCore.Mvc;
using data_api.Services;
using data_api.Models;

namespace data_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillController : ControllerBase
{
    private readonly IDataService<Skill> _service;

    public SkillController(IDataService<Skill> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Skill>>> GetAll()
    {
        var skills = await _service.GetAllDataAsync();
        return Ok(skills); // produces status 200 ('ok')
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Skill>> GetById(string id)
    {
        var skill = await _service.GetDataByIdAsync(id);
        if (skill == null)
        {
            return NotFound();
        }
        return Ok(skill);
    }

    [HttpPost]
    public async Task<ActionResult<Skill>> Create([FromBody] Skill skill)
    {
        await _service.CreateDataAsync(skill);
        return CreatedAtAction(nameof(GetById), new { id = skill.Id }, skill); // returns status 201 ('created response')
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Skill skill)
    {
        var existingData = await _service.GetDataByIdAsync(id);
        if (existingData == null)
        {
            return NotFound();
        }
        await _service.UpdateDataAsync(id, skill);
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