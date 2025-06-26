using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Contexts.Configurations;

public class TipoPostConfiguration : IEntityTypeConfiguration<TipoPost>
{
    public void Configure(EntityTypeBuilder<TipoPost> builder)
    {
        builder.ToTable("TiposPost");
    }
}
