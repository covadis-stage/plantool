using Microsoft.EntityFrameworkCore;
using plantool.Domain.Entities;
using plantool.Data.Configurations;

namespace plantool.Data;

public class PlantoolDbContext(DbContextOptions<PlantoolDbContext> options) : DbContext(options)
{
    public DbSet<ActivityType> ActivityTypes { get; set; }
    public DbSet<CompetenceTeam> CompetenceTeams { get; set; }
    public DbSet<Engineer> Engineers { get; set; }
    public DbSet<ProductTeam> ProductTeams { get; set; }
    public DbSet<ProjectActivity> Activities { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<WorkCenter> WorkCenters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ActivityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CompetenceTeamConfiguration());
        modelBuilder.ApplyConfiguration(new EngineerTeamConfiguration());
        modelBuilder.ApplyConfiguration(new ProductTeamConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectActivityConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new WorkCenterConfiguration());
    }
}
