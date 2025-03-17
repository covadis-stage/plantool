namespace plantool.Models;

public class WorkBreakdownStructure
{
    public string Id { get; set; }
    public string Type { get; set; }
    public List<ProjectActivity> ProjectActivities { get; set; }

    public string NetworkId { get; set; }
    public Network Network { get; set; } = null!;
}

