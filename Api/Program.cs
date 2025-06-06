using Api.Configurations;
using Api.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = builder.Environment;

// Add services to the container.
builder.Services
    .AddDbContext<PostgresDbContext>()
    .AddDependencyInjection()
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

// Configura o Kestrel para escutar apenas em HTTP na porta 8080
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); // Escuta em todas as interfaces na porta 8080 (HTTP)
});

var app = builder.Build();

// Aplica as migrations ao iniciar a aplicação
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<PostgresDbContext>();
    context.Database.Migrate(); // Aplica todas as migrations pendentes ao banco de dados
                                // Opcional: Você pode adicionar um log aqui para indicar que a migração foi bem-sucedida
    Console.WriteLine("Migrations aplicadas com sucesso!");
}
catch (Exception ex)
{
    // Opcional: Adicione um log de erro mais detalhado aqui
    Console.WriteLine($"Ocorreu um erro ao aplicar as migrations: {ex.Message}");
    // Considere interromper a inicialização da aplicação em caso de falha crítica
    // throw;
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();