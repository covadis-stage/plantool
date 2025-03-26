namespace plantool.Domain.Entities;

public class Project : IAuditable
{
    // Imported from SAP
    public string Key { get; set; } = null!; // = ProjectId
    public string? Customer { get; set; }
    public string? ProjectManager { get; set; }
    public ICollection<ProjectActivity> Activities { get; set; } = [];

    // Updated automatically
    public bool IsArchived { get; set; }
}
