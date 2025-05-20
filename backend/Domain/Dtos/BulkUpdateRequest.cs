namespace plantool.Domain.Dtos;

public class BulkUpdateRequest
{
    public List<string> ActivityKeys { get; set; } = new List<string>();
    public string? DelegatorId { get; set; } // PL. Workcenter
    public string? EngineerId { get; set; } // PL. Engineer
    public DateOnly? ActualFinishDate { get; set; }
    public DateOnly? ActualStartDate { get; set; }
    
    public Dictionary<string, string> FindProperties()
    {
        var properties = new Dictionary<string, string>();

        if (DelegatorId != null) properties.Add(nameof(DelegatorId), DelegatorId);
        if (EngineerId != null) properties.Add(nameof(EngineerId), EngineerId);
        if (ActualFinishDate != null) properties.Add(nameof(ActualFinishDate), ActualFinishDate?.ToString("yyyy-MM-dd") ?? string.Empty);
        if (ActualStartDate != null) properties.Add(nameof(ActualStartDate), ActualStartDate?.ToString("yyyy-MM-dd") ?? string.Empty);

        return properties;
    }
}
