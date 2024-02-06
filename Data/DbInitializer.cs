using TodoApi.Models;

namespace TodoApi.Data;
public static class DbInitializer
{
  public static async Task InitializeAsync(TodoDbContext context)
  {
    await context.Database.EnsureCreatedAsync();

    if (context.TodoItems.Any())
    {
      return;
    }

    var todos = new TodoItem[]
    {
            new() { Name = "C# Study", IsComplete = false },
            new() { Name = "TypeScript Study", IsComplete = false },
    };

    await context.TodoItems.AddRangeAsync(todos);
    await context.SaveChangesAsync();
  }
}
