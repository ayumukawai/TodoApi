using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
{
    public DbSet<TodoItem> TodoItems { get; set; } = null!;
}