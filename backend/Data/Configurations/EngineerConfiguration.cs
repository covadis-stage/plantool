using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using plantool.Domain.Entities;

namespace plantool.Data.Configurations;

public class EngineerTeamConfiguration : IEntityTypeConfiguration<Engineer>
{
    public void Configure(EntityTypeBuilder<Engineer> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .IsRequired()
            .HasMaxLength(100)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .IsRequired();

        builder.Property(e => e.CanDelegate)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(e => e.ProductTeam)
            .WithMany(pt => pt.Engineers)
            .HasForeignKey(e => e.ProductTeamName)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
