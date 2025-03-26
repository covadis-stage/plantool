using System.Globalization;
using plantool.Domain;
using plantool.Domain.Entities;

namespace plantool.Services.CsvService;

public static class CsvMappingService
{
    public static MappedCsvData MapCsv(List<string> columns, List<string[]> csvRows)
    {
        var formattedRows = FormatRows(columns, csvRows);

        var projects = FindProjects(formattedRows);
        var activityTypes = FindActivityTypes(formattedRows);
        var projectActivities = FindProjectActivities(projects, activityTypes, formattedRows);

        return new MappedCsvData(
            projects: projects,
            activityTypes: activityTypes,
            projectActivities: projectActivities
        );
    }

    private static List<CsvRow> FormatRows(List<string> columns, List<string[]> csvRows)
    {
        var projectIdIndex = columns.IndexOf("Project");
        var networkIdIndex = columns.IndexOf("Network");
        var activityIdIndex = columns.IndexOf("Activity");
        var customerIndex = columns.IndexOf("Customer");
        var wbsIdIndex = columns.IndexOf("WBS");
        var operationShortTextIndex = columns.IndexOf("Opr. short text");
        var projectManagerIndex = columns.IndexOf("PM");
        var teamLeaderIndex = columns.IndexOf("TL");
        var latestStartDateIndex = columns.IndexOf("Latest Start Date");
        var latestFinishDateIndex = columns.IndexOf("Latest Finish Date");
        var activityCodeIndex = columns.IndexOf("Activity Code");
        var originalFinishDateIndex = columns.IndexOf("Original Finish Date");
        var activityDescriptionIndex = columns.IndexOf("Activity Description");
        var actualWorkIndex = columns.IndexOf("Actual work");
        var workIndex = columns.IndexOf("Work");
        var workCenterIndex = columns.IndexOf("Work Center");
        var wbsTypeIndex = columns.IndexOf("WBS Type");

        List<CsvRow> formattedCsvRows = [];
        csvRows.ForEach(row =>
            formattedCsvRows.Add(new CsvRow(
                projectId: row[projectIdIndex],
                networkId: row[networkIdIndex],
                activityId: row[activityIdIndex],
                customer: row[customerIndex],
                wbsId: row[wbsIdIndex],
                operationShortText: row[operationShortTextIndex],
                projectManager: row[projectManagerIndex],
                teamLeader: row[teamLeaderIndex],
                latestStartDate: row[latestStartDateIndex],
                latestFinishDate: row[latestFinishDateIndex],
                activityCode: row[activityCodeIndex],
                originalFinishDate: row[originalFinishDateIndex],
                activityDescription: row[activityDescriptionIndex],
                actualWork: row[actualWorkIndex],
                work: row[workIndex],
                workCenter: row[workCenterIndex],
                wbsType: row[wbsTypeIndex]
            ))
        );

        return formattedCsvRows;
    }

    private static List<Project> FindProjects(List<CsvRow> formattedRows)
    {
        return formattedRows
        .Where(row => row.ProjectId != null)
        .Select(row =>
            new Project
            {
                Key = row.ProjectId!,
                Customer = row.Customer,
                ProjectManager = row.ProjectManager,
            }
        ).Distinct().ToList();
    }

    private static List<ActivityType> FindActivityTypes(List<CsvRow> formattedRows)
    {
        return formattedRows
        .Where(row => row.ActivityCode != null)
        .Select(row =>
            new ActivityType
            {
                Key = row.ActivityCode!,
                Description = row.ActivityDescription,
                OperationShortText = row.OperationShortText,
            }
        ).Distinct().ToList();
    }

    private static List<ProjectActivity> FindProjectActivities(List<Project> projects, List<ActivityType> activityTypes, List<CsvRow> formattedRows)
    {
        var projectActivities = formattedRows
        .Where(row =>
            row.ActivityId != null
            && row.NetworkId != null
            && row.ProjectId != null
            && projects.Any(project => project.Key == row.ProjectId)
        )
        .Select(row => 
            new ProjectActivity 
            {
                Key = $"{row.ProjectId!}-{row.NetworkId!}-{row.ActivityId!}",
                Id = row.ActivityId!,
                LatestStartDate = ParseDate(row.LatestStartDate),
                LatestFinishDate = ParseDate(row.LatestFinishDate),
                OriginalFinishDate = ParseDate(row.OriginalFinishDate),
                TimeEstimated = ParseTimeSpan(row.Work),
                TimeSpent = ParseTimeSpan(row.ActualWork),
                TeamLeader = row.TeamLeader,
                ActivityTypeCode = row.ActivityCode,
                ActivityType = activityTypes.FirstOrDefault(activityType => activityType.Key == row.ActivityCode),
                WorkBreakdownStructure = row.WbsId,
                Network = row.NetworkId,
                ProjectId = row.ProjectId!,
                Project = projects.First(project => project.Key == row.ProjectId),
            }
        ).Distinct().ToList();

        // Add self as child to parent project
        projectActivities.ForEach(projectActivity =>
        {
            projectActivity.Project.Activities ??= [];
            projectActivity.Project.Activities.Add(projectActivity);
        });

        return projectActivities;
    }

    // Format should be yyyyMMdd. Example: 20250927
    private static DateOnly? ParseDate(string? date)
    {
        if (date == null) return null;
        if (date == "") return null;
        if (date == "00000000") return null;
        if (!int.TryParse(date, out int dateAsNumber) || dateAsNumber < 10000000) return null;
        return DateOnly.FromDateTime(DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture));
    }

    // Format should be HH with a dot as decimal seperator. Example: 12.500 = 12 hours and 30 minutes
    private static TimeSpan? ParseTimeSpan(string? time)
    {
        if (time == null) return null;
        if (time == "") return null;
        if (!double.TryParse(time, NumberStyles.Any, CultureInfo.InvariantCulture, out double timeAsNumber)) return null;
        return TimeSpan.FromHours(timeAsNumber);
    }
}