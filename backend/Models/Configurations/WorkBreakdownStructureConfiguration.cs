using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using plantool.Models.Entities;

namespace plantool.Models.Configurations;

public class WorkBreakdownStructureConfiguration : IEntityTypeConfiguration<WorkBreakdownStructure>
{
    public void Configure(EntityTypeBuilder<WorkBreakdownStructure> builder)
    {
        builder.HasKey(wbs => wbs.Id);
        builder.Property(wbs => wbs.Id)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(wbs => wbs.Network)
            .WithMany(p => p.WorkBreakdownStructures)
            .HasForeignKey(wbs => wbs.NetworkId);
    }
}
