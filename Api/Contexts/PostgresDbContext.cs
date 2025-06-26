﻿using Api.Contexts.Configurations;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Contexts;

public class PostgresDbContext(IConfiguration configuration, DbContextOptions<PostgresDbContext> options) : DbContext(options)
{
    private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

    public DbSet<TipoPost> TipoPost { get; set; }
    public DbSet<Post> Post { get; set; }
    public DbSet<Menu> Menu { get; set; }
    public DbSet<Anuncio> Anuncio { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TipoPostConfiguration());
        modelBuilder.ApplyConfiguration(new MenuConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new AnuncioConfiguration());
    }
}
