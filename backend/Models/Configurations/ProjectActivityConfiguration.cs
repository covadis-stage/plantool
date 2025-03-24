using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using plantool.Models.Entities;

namespace plantool.Models.Configurations;

public class ProjectActivityConfiguration : IEntityTypeConfiguration<ProjectActivity>
{
    public void Configure(EntityTypeBuilder<ProjectActivity> builder)
    {
        builder.HasKey(pa => pa.Id);
        builder.Property(pa => pa.Id)
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