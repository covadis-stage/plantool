namespace plantool.Models.Entities;

public class WorkBreakdownStructure
{
    public string Id { get; set; }
    public string Type { get; set; }
    public ICollection<ProjectActivity> ProjectActivities { get; set; }

    public string NetworkId { get; set; }
    public Network Network { get; set; } = null!;
}

