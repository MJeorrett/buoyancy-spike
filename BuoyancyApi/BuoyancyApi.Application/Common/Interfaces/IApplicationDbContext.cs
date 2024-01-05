using BuoyancyApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuoyancyApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<RoleEntity> Roles { get; }
    public DbSet<ProjectEntity> Projects { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
