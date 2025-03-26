namespace plantool.Domain.Entities;
public class ProjectActivity : IAuditable
{
    // Imported from SAP
    public string Key { get; set; } = null!; // = ProjectId + NetworkId + ActivityId
    public string Id { get; set; } = null!; // "Activity"(Id) in SAP
    public DateOnly? LatestStartDate { get; set; }
    public DateOnly? LatestFinishDate { get; set; }
    public DateOnly? OriginalFinishDate { get; set; }
    public TimeSpan? TimeEstimated { get; set; } // "Work" in SAP
    public TimeSpan? TimeSpent { get; set; } // "Actual Work" in SAP
    public string? TeamLeader { get; set; } // = Product Lead
    
    public string? ActivityTypeCode { get; set; }
    public ActivityType? ActivityType { get; set; }

    public string? WorkBreakdownStructure { get; set; }
    public string? Network { get; set; }

    public string ProjectId { get; set; } = null!;
    public Project Project { get; set; } = null!;

    // Updated automatically
    public bool IsArchived { get; set; }

    // Written by user
    public string? GeneralRemark { get; set; }
    public DateOnly? ActualStartDate { get; set; } // PL. Latest Start Date
    public DateOnly? ActualFinishDate { get; set; } // PL. Latest Finish Date
    public TimeSpan? AbsoluteWorkload { get; set; } // PL. Absolute Workload

}

