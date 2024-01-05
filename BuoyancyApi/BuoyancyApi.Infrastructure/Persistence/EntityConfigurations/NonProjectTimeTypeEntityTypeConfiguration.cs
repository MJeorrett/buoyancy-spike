using BuoyancyApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuoyancyApi.Infrastructure.Persistence.EntityConfigurations;

public class NonProjectTimeTypeEntityTypeConfiguration : IEntityTypeConfiguration<NonProjectTimeTypeEntity>
{
    public void Configure(EntityTypeBuilder<NonProjectTimeTypeEntity> builder)
    {
        builder.ToTable("NonProjectTimeType");

        builder.Property(_ => _.Id)
            .HasColumnName("NonProjectTimeTypeId");

    }
}
