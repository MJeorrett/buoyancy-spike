using BuoyancyApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuoyancyApi.Infrastructure.Persistence.EntityConfigurations;

public class RequiredTimeEntityTypeConfiguration : IEntityTypeConfiguration<RequiredTimeEntity>
{
    public void Configure(EntityTypeBuilder<RequiredTimeEntity> builder)
    {
        builder.ToTable("RequiredTime");

        builder.Property(_ => _.Id)
            .HasColumnName("RequiredTimeId");

    }
}
