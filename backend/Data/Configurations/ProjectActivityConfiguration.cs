using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using plantool.Domain.Entities;

namespace plantool.Data.Configurations;

public class ProjectActivityConfiguration : IEntityTypeConfiguration<ProjectActivity>
{
    public void Configure(EntityTypeBuilder<ProjectActivity> builder)
    {
        builder.HasKey(pa => pa.Key);
        builder.Property(pa => pa.Key)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(pa => pa.ActivityType)
            .WithMany()
            .HasForeignKey(pa => pa.ActivityTypeCode);

        builder.HasOne(pa => pa.WorkCenter)
            .WithMany()
            .HasForeignKey(pa => pa.WorkCenterKey);

        builder.HasOne(pa => pa.Project)
            .WithMany(p => p.Activities)
            .HasForeignKey(pa => pa.ProjectId);

        builder.Property(pa => pa.TimeEstimated)
            .HasConversion<long?>();

        builder.Property(pa => pa.TimeSpent)
            .HasConversion<long?>();
    }
}