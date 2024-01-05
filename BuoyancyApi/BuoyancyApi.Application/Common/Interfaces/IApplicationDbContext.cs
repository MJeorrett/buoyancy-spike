using BuoyancyApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuoyancyApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<RoleEntity> Roles { get; }
    public DbSet<SkillEntity> Skills { get; }
    public DbSet<PersonSkillEntity> PersonSkills { get; }
    public DbSet<PersonEntity> Persons { get; }
    public DbSet<NonProjectTimeTypeEntity> NonProjectTimeTypes { get; }
    public DbSet<PlannedTimeEntity> PlannedTimes { get; }
    public DbSet<RequiredTimeEntity> RequiredTimes { get; }
    public DbSet<ProjectEntity> Projects { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
