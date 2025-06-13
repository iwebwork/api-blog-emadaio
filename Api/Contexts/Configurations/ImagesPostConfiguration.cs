using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Contexts.Configurations;

public class ImagesPostConfiguration : IEntityTypeConfiguration<ImagesPost>
{
    public void Configure(EntityTypeBuilder<ImagesPost> builder)
    {
        builder.ToTable("ImagesPosts");

    }
}
