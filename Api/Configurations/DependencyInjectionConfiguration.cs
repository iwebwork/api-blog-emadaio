using Api.Contexts;
using Api.Repositories;
using Infraestrutura.Controllers;

namespace Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IResponseControler, ResponseControler>();
        services.AddDbContext<SqliteDbContext>();

        services.AddScoped<IPostRepository, PostRepository>();
        return services;
    }
}
