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
    .AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    })
    .AddEndpointsApiExplorer()
    .AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();


app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
