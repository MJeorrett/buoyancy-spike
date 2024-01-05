using BuoyancyApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuoyancyApi.Infrastructure.Persistence.EntityConfigurations;

public class PersonSkillEntityTypeConfiguration : IEntityTypeConfiguration<PersonSkillEntity>
{
    public void Configure(EntityTypeBuilder<PersonSkillEntity> builder)
    {
        builder.ToTable("PersonSkill");

        builder.Property(_ => _.Id)
            .HasColumnName("PersonSkillId");

    }
}
