using BuoyancyApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuoyancyApi.Infrastructure.Persistence.EntityConfigurations;

public class BuoyancyApiEntityTypeConfiguration : IEntityTypeConfiguration<ProjectEntity>
{
    public void Configure(EntityTypeBuilder<ProjectEntity> builder)
    {
        builder.ToTable("Project");

        builder.Property(_ => _.Id)
            .HasColumnName("ProjectId");

        builder.Property(_ => _.Title)
            .HasColumnType("nvarchar(256)");
    }
}
