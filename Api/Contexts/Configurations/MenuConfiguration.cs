using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Contexts.Configurations;

public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus");
        builder.HasIndex(i => i.Label).IsUnique();
        builder.HasIndex(i => i.Url).IsUnique();
        builder.HasIndex(i => i.TipoPostId).IsUnique();
    }
}
