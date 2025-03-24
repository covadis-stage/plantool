namespace plantool.Domain.Entities;
public class Network
{
    public string Id { get; set; } = null!;
    public ICollection<WorkBreakdownStructure> WorkBreakdownStructures { get; set; } = [];

    public string? ProjectId { get; set; }
    public Project? Project { get; set; }
}
