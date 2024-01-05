using BuoyancyApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuoyancyApi.Infrastructure.Persistence.EntityConfigurations;

public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<PersonEntity>
{
    public void Configure(EntityTypeBuilder<PersonEntity> builder)
    {
        builder.ToTable("Person");

        builder.Property(_ => _.Id)
            .HasColumnName("PersonId");

    }
}
