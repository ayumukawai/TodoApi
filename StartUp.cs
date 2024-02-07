using TodoApi.Services;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.UnitOfWork;

namespace YourNamespace
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddDbContext<TodoDbContext>(opt =>
          opt.UseInMemoryDatabase("TodoList"));
      services.AddCors(options =>
    {
      options.AddPolicy("AllowSpecificOrigin", builder =>
      {
        builder.WithOrigins("http://localhost:3000")
                 .AllowAnyMethod()
                 .AllowAnyHeader();
      });
    });
      services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<ITodoService, TodoService>();
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthorization();
      app.UseCors("AllowSpecificOrigin");
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
