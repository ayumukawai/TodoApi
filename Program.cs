using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi;
public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<TodoDbContext>();
                DbInitializer.InitializeAsync(context).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while seeding the database.");
                Console.WriteLine(ex.Message);
            }
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
