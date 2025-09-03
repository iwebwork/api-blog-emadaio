using Api.Contexts;
using Api.Models;
using Infraestrutura.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Configurations;

public static class IdentityConfiguration
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), a => a.MigrationsHistoryTable(HistoryRepository.DefaultTableName, BaseDbContext.Schema)));

        services.AddIdentityApiEndpoints<Usuario>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
