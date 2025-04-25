namespace plantool.Domain.Dtos;
public class BulkUpdateRequest
{
    public List<string> ActivityKeys { get; set; } = new List<string>();
    public string? DelegatorId { get; set; } // PL. Workcenter
    public string? EngineerId { get; set; } // PL. Engineer
    public DateOnly? ActualFinishDate { get; set; }
    public DateOnly? ActualStartDate { get; set; }
}
