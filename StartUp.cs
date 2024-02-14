using Microsoft.EntityFrameworkCore;
using TodoApi.Services;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.UnitOfWork;

namespace TodoApi;
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
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IUnitOfWork, ProjectUnitOfWork>();
    services.AddScoped<ITodoService, TodoService>();
    services.AddScoped<IAuthService, AuthService>();
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
