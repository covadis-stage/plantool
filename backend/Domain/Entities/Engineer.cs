namespace plantool.Domain.Entities;
public class Engineer
{
    public string Id {get; set; } = null!;
    public string Name {get; set; } = null!;
    public bool CanDelegate { get; set; }
    
    public string ProductTeamName { get; set; } = null!;
    public ProductTeam ProductTeam { get; set; } = null!;
}
