using Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddDependencyInjection()
    .AddEndpointsApiExplorer()
    .AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthorization();

app.Run();
