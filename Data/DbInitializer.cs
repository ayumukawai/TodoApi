using TodoApi.Models;

namespace TodoApi.Data;
public static class DbInitializer
{
  public static async Task InitializeAsync(TodoDbContext context)
  {
    await context.Database.EnsureCreatedAsync();

    var users = new User[]
    {
            new() {UserName = "田中太郎", Password = "hogehoge"},
            new() {UserName = "山田花子", Password = "hugahuga"},
    };
    await context.Users.AddRangeAsync(users);
    await context.SaveChangesAsync();

    var todos = new TodoItem[]
    {
            new() { Name = "C# Study", IsComplete = false },
            new() { Name = "TypeScript Study", IsComplete = false },
            new() { Name = "React Study", IsComplete = false },
            new() { Name = ".NET Study", IsComplete = false },
            new() { Name = "Next.js Study", IsComplete = false },
    };
    await context.TodoItems.AddRangeAsync(todos);
    await context.SaveChangesAsync();
  }
}
