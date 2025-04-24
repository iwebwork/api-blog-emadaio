using Api.Models;
using Infraestrutura.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Contexts;

public class SqliteDbContext(DbContextOptions options) : BaseDbContext(options)
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<ImagesPost> ImagesPost { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        SqliteSettings sqliteSettings = new("BlogEmadaio");
        optionsBuilder.UseSqlite(sqliteSettings.GetConnectionString());
    }
}
