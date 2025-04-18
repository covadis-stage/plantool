namespace plantool.Domain.Entities;
public class CompetenceTeam
{
    public string Name { get; set; } = null!;

    public ICollection<ProductTeam> ProductTeams { get; set; } = [];
}

