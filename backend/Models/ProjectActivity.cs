namespace plantool.Models;
public class ProjectActivity
{
    // Imported from SAP
    public string Id { get; set; }
    public DateOnly? LatestStartDate { get; set; }
    public DateOnly? LatestFinishDate { get; set; }
    public DateOnly? OriginalFinishDate { get; set; }
    public TimeSpan? TimeEstimated { get; set; } // "Work" in SAP
    public TimeSpan? TimeSpent { get; set; } // "Actual Work" in SAP
    public string? TeamLeader { get; set; } // = Product Lead
    
    public string ActivityTypeCode { get; set; }
    public ActivityType ActivityType { get; set; } = null!;

    public string WorkBreakdownStructureId { get; set; }
    public WorkBreakdownStructure WorkBreakdownStructure { get; set; } = null!;

    // Written by user
    public string? GeneralRemark { get; set; }
    public DateOnly? ActualStartDate { get; set; } // PL. Latest Start Date
    public DateOnly? ActualFinishDate { get; set; } // PL. Latest Finish Date
    public TimeSpan? AbsoluteWorkload { get; set; } // PL. Absolute Workload

}

