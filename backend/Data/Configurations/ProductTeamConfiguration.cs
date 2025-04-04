using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using plantool.Domain.Entities;

namespace plantool.Data.Configurations;

public class ProductTeamConfiguration : IEntityTypeConfiguration<ProductTeam>
{
    public void Configure(EntityTypeBuilder<ProductTeam> builder)
    {
        builder.HasKey(pt => pt.Name);
        builder.Property(pt => pt.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(pt => pt.CompetenceTeam)
            .WithMany(ct => ct.ProductTeams)
            .HasForeignKey(pt => pt.CompetenceTeamName);

        builder.HasOne(pt => pt.ProductLead)
            .WithMany()
            .HasForeignKey(pt => pt.ProductLeadId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
