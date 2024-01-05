using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BuoyancyApi.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
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
}
