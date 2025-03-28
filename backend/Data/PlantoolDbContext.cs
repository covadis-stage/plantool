using Microsoft.EntityFrameworkCore;
using plantool.Domain.Entities;
using plantool.Data.Configurations;

namespace plantool.Data;

public class PlantoolDbContext(DbContextOptions<PlantoolDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectActivity> Activities { get; set; }
    public DbSet<ActivityType> ActivityTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ActivityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectActivityConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
    }
}
