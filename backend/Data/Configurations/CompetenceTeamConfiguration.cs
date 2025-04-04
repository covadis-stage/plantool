using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using plantool.Domain.Entities;

namespace plantool.Data.Configurations;

public class CompetenceTeamConfiguration : IEntityTypeConfiguration<CompetenceTeam>
{
    public void Configure(EntityTypeBuilder<CompetenceTeam> builder)
    {
        builder.HasKey(ct => ct.Name);
        builder.Property(ct => ct.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
