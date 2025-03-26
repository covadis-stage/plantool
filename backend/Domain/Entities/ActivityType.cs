namespace plantool.Domain.Entities;
public class ActivityType : IAuditable
{
    // Imported from SAP
    public string Key { get; set; } = null!; // = Activity Code
    public string? Description { get; set; }
    public string? OperationShortText { get; set; }

    // Updated automatically
    public bool IsArchived { get; set; }
}

