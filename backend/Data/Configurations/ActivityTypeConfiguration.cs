using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using plantool.Domain.Entities;

namespace plantool.Data.Configurations;

public class ActivityTypeConfiguration : IEntityTypeConfiguration<ActivityType>
{
    public void Configure(EntityTypeBuilder<ActivityType> builder)
    {
        builder.HasKey(at => at.Key);
        builder.Property(at => at.Key)
            .IsRequired()
            .HasMaxLength(20);
    }
}
