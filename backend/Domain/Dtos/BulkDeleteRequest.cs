
namespace plantool.Domain.Dtos;
public class BulkDeleteRequest
{
    public List<string> ActivityKeys { get; set; } = new List<string>();
    public bool? Delegator { get; set; } // PL. Workcenter
    public bool? Engineer { get; set; } // PL. Engineer
    public bool? ActualFinishDate { get; set; }
    public bool? ActualStartDate { get; set; }

    public List<string> FindProperties()
    {
        var properties = new List<string>();
        
        if (Delegator == true) properties.Add(nameof(Delegator));
        if (Engineer == true) properties.Add(nameof(Engineer));
        if (ActualFinishDate == true) properties.Add(nameof(ActualFinishDate));
        if (ActualStartDate == true) properties.Add(nameof(ActualStartDate));

        return properties;
    }
}
