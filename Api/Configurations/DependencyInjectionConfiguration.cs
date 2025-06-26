using Api.Contexts;
using Api.Repositories.Implementations;
using Api.Repositories.Interfaces;
using Infraestrutura.Controllers;

namespace Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IResponseControler, ResponseControler>();
        services.AddDbContext<PostgresDbContext>();

        services.AddScoped<IAnuncioRepository, AnuncioRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ITipoPostRepository, TipoPostRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();

        return services;
    }
}
