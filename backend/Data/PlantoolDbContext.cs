using Microsoft.EntityFrameworkCore;
using plantool.Domain.Entities;
using plantool.Domain.Configurations;

namespace plantool.Data;

public class PlantoolDbContext : DbContext
{
    public PlantoolDbContext(DbContextOptions<PlantoolDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectActivity> Activities { get; set; }
    public DbSet<ActivityType> ActivityTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ActivityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new NetworkConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectActivityConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new WorkBreakdownStructureConfiguration());
    }
}
