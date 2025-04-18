namespace plantool.Domain.Entities;

public class WorkCenter : IAuditable
{
    public string Key { get; set; } = null!;
    public string? ReadableName { get; set; }    
    public bool IsArchived { get; set; }
}