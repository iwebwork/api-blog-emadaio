using Api.Configurations;
using Api.Contexts;
using Infraestrutura.Context;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services
    .AddSqlite<SqliteDbContext>()
    .AddDependencyInjection()
    .AddDbContext<SqliteDbContext>()
    .AddEndpointsApiExplorer()
    .AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthorization();

app.Run();
