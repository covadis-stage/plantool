using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using plantool.Domain.Entities;

namespace plantool.Domain.Configurations;

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

        builder.HasOne(pa => pa.WorkBreakdownStructure)
            .WithMany(wbs => wbs.ProjectActivities)
            .HasForeignKey(pa => pa.WorkBreakdownStructureId);
    }
}