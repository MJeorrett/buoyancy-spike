using BuoyancyApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuoyancyApi.Infrastructure.Persistence.EntityConfigurations;

public class PlannedTimeEntityTypeConfiguration : IEntityTypeConfiguration<PlannedTimeEntity>
{
    public void Configure(EntityTypeBuilder<PlannedTimeEntity> builder)
    {
        builder.ToTable("PlannedTime");

        builder.Property(_ => _.Id)
            .HasColumnName("PlannedTimeId");

    }
}
