using TodoApi.Services;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController(ITodoService todoService) : ControllerBase
{
  private readonly ITodoService _todoService = todoService;

  [HttpGet]
  public async Task<ActionResult<IEnumerable<TodoItem>>> Get()
  {
    var entities = await _todoService.GetAllAsync();
    return Ok(entities);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<TodoItem>> GetById(int id)
  {
    return await _todoService.GetByIdAsync(id);
  }

  [HttpPost]
  public async Task<ActionResult> Post([FromBody] TodoItem entity)
  {
    await _todoService.AddAsync(entity);
    return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> Put(int id, [FromBody] TodoItem entity)
  {
    if (id != entity.Id) return BadRequest();

    await _todoService.UpdateAsync(entity);
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> Delete(int id)
  {
    await _todoService.DeleteAsync(id);
    return NoContent();
  }
}