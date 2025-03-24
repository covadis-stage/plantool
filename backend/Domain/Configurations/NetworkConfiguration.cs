using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using plantool.Domain.Entities;

namespace plantool.Domain.Configurations;

public class NetworkConfiguration : IEntityTypeConfiguration<Network>
{
    public void Configure(EntityTypeBuilder<Network> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(n => n.Project)
            .WithMany(p => p.Networks)
            .HasForeignKey(n => n.ProjectId);
    }
}
