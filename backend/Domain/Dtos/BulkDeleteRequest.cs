namespace plantool.Domain.Dtos;
public class BulkDeleteRequest
{
    public List<string> ActivityKeys { get; set; } = new List<string>();
    public bool? Delegator { get; set; } // PL. Workcenter
    public bool? Engineer { get; set; } // PL. Engineer
    public bool? ActualFinishDate { get; set; }
    public bool? ActualStartDate { get; set; }
}
