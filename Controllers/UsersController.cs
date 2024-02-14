using TodoApi.Services;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        var entities = await _userService.GetAllAsync();
        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(int id)
    {
        return await _userService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] User entity)
    {
        await _userService.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.UserId }, entity);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] User entity)
    {
        if (id != entity.UserId) return BadRequest();

        await _userService.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
}