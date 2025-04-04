namespace plantool.Domain.Entities;
public class ProductTeam
{
    public string Name { get; set; } = null!;
    
    public string CompetenceTeamName { get; set; } = null!;
    public CompetenceTeam CompetenceTeam { get; set; } = null!;

    public string ProductLeadId { get; set; } = null!;
    public Engineer ProductLead { get; set; } = null!;

    public ICollection<Engineer> Engineers { get; set; } = [];
}

