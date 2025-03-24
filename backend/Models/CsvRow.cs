namespace plantool.Models;

public class CsvRow(string? projectId, string? networkId, string? activityId, string? customer, string? wbsId, string? operationShortText, string? projectManager, string? teamLeader, string? latestStartDate, string? latestFinishDate, string? activityCode, string? originalFinishDate, string? activityDescription, string? actualWork, string? work, string? workCenter, string? wbsType)
{
    public string? ProjectId { get; set; } = projectId;
    public string? NetworkId { get; set; } = networkId;
    public string? ActivityId { get; set; } = activityId;
    public string? Customer { get; set; } = customer;
    public string? WbsId { get; set; } = wbsId;
    public string? OperationShortText { get; set; } = operationShortText;
    public string? ProjectManager { get; set; } = projectManager;
    public string? TeamLeader { get; set; } = teamLeader;
    public string? LatestStartDate { get; set; } = latestStartDate;
    public string? LatestFinishDate { get; set; } = latestFinishDate;
    public string? ActivityCode { get; set; } = activityCode;
    public string? OriginalFinishDate { get; set; } = originalFinishDate;
    public string? ActivityDescription { get; set; } = activityDescription;
    public string? ActualWork { get; set; } = actualWork;
    public string? Work { get; set; } = work;
    public string? WorkCenter { get; set; } = workCenter;
    public string? WbsType { get; set; } = wbsType;
}