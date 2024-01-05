using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;
using BuoyancyApi.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BuoyancyApi.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<RoleEntity> Roles { get; init; } = null!;
    public DbSet<SkillEntity> Skills { get; init; } = null!;
    public DbSet<PersonSkillEntity> PersonSkills { get; init; } = null!;
    public DbSet<PersonEntity> Persons { get; init; } = null!;
    public DbSet<NonProjectTimeTypeEntity> NonProjectTimeTypes { get; init; } = null!;
    public DbSet<PlannedTimeEntity> PlannedTimes { get; init; } = null!;
    public DbSet<RequiredTimeEntity> RequiredTimes { get; init; } = null!;
    public DbSet<ProjectEntity> Projects { get; init; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        base.ConfigureConventions(builder);

        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");

        builder.Properties<DateTime>()
            .HaveConversion<DateTimeConverter>()
            .HaveColumnType("datetime2");
    }
}
