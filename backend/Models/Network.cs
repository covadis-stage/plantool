namespace plantool.Models;
public class Network
{
    public string Id { get; set; }
    public List<WorkBreakdownStructure> WorkBreakdownStructures { get; set; }

    public string ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}
