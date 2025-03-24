using System.Globalization;
using plantool.Models;
using plantool.Models.Entities;

namespace plantool.Services;

public class CsvProcessingService
{
    private readonly PlantoolDbContext _context;
    private readonly ILogger<CsvProcessingService> _logger;

    public CsvProcessingService(PlantoolDbContext context, ILogger<CsvProcessingService> logger) => (_context, _logger) = (context, logger);

    public async Task ProcessCsv()
    {
        _logger.LogInformation("Reading CSV file...");

        var csvFile = Path.Combine(Directory.GetCurrentDirectory(), "InputData", "Engineering Orderplanning.csv");
        var csvData = await File.ReadAllLinesAsync(csvFile);
        var columns = csvData[0].Split(';').ToList();
        var csvRows = csvData.Skip(1).Select(row => row.Split(';')).ToList();

        _logger.LogInformation("CSV file read, processing...");

        var formattedRows = FormatRows(columns, csvRows);

        var projects = FindProjects(formattedRows);
        var networks = FindNetworks(projects, formattedRows);
        var wbs = FindWorkBreakdownStructures(networks, formattedRows);
        var activityTypes = FindActivityTypes(formattedRows);
        var projectActivities = FindProjectActivities(wbs, activityTypes, formattedRows);

        _logger.LogInformation("CSV file processed");
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
                Id = row.ProjectId!,
                Customer = row.Customer,
                ProjectManager = row.ProjectManager,
            }
        ).Distinct().ToList();
    }

    private static List<Network> FindNetworks(List<Project> projects, List<CsvRow> formattedRows)
    {
        var networks = formattedRows
        .Where(row => row.NetworkId != null)
        .Select(row =>
            new Network
            {
                Id = row.NetworkId!,
                ProjectId = row.ProjectId,
                Project = projects.FirstOrDefault(project => project.Id == row.ProjectId),
            }
        ).Distinct().ToList();

        // Add self as child to parent project
        networks.ForEach(network =>
        {
            if (network.Project == null) return;
            network.Project!.Networks ??= [];
            network.Project!.Networks.Add(network);
        });

        return networks;
    }

    private static List<WorkBreakdownStructure> FindWorkBreakdownStructures(List<Network> networks, List<CsvRow> formattedRows)
    {
        var wbs = formattedRows
        .Where(row => row.WbsId != null)
        .Select(row =>
            new WorkBreakdownStructure
            {
                Id = row.WbsId!,
                Type = row.WbsType,
                NetworkId = row.NetworkId,
                Network = networks.FirstOrDefault(network => network.Id == row.NetworkId),
            }
        ).Distinct().ToList();

        // Add self as child to parent network
        wbs.ForEach(wbs =>
        {
            if (wbs.Network == null) return;
            wbs.Network!.WorkBreakdownStructures ??= [];
            wbs.Network!.WorkBreakdownStructures.Add(wbs);
        });

        return wbs;
    }

    private static List<ActivityType> FindActivityTypes(List<CsvRow> formattedRows)
    {
        return formattedRows
        .Where(row => row.ActivityCode != null)
        .Select(row =>
            new ActivityType
            {
                Code = row.ActivityCode!,
                Description = row.ActivityDescription,
                OperationShortText = row.OperationShortText,
            }
        ).Distinct().ToList();
    }

    private static List<ProjectActivity> FindProjectActivities(List<WorkBreakdownStructure> wbs, List<ActivityType> activityTypes, List<CsvRow> formattedRows)
    {
        var projectActivities = formattedRows
        .Where(row =>
            row.ActivityId != null
            && row.WbsId != null
            && row.NetworkId != null
            && row.ProjectId != null
            && wbs.Any(wbs => wbs.Id == row.WbsId)
        )
        .Select(row =>
        {
            return new ProjectActivity
            {
                Key = $"{row.ProjectId!}-{row.NetworkId!}-{row.WbsId!}-{row.ActivityId!}",
                Id = row.ActivityId!,
                LatestStartDate = ParseDate(row.LatestStartDate),
                LatestFinishDate = ParseDate(row.LatestFinishDate),
                OriginalFinishDate = ParseDate(row.OriginalFinishDate),
                TimeEstimated = ParseTimeSpan(row.Work),
                TimeSpent = ParseTimeSpan(row.ActualWork),
                TeamLeader = row.TeamLeader,
                ActivityTypeCode = row.ActivityCode,
                ActivityType = activityTypes.FirstOrDefault(activityType => activityType.Code == row.ActivityCode),
                WorkBreakdownStructureId = row.WbsId!,
                WorkBreakdownStructure = wbs.First(wbs => wbs.Id == row.WbsId),
            };
        }).Distinct().ToList();

        // Add self as child to parent wbs
        projectActivities.ForEach(projectActivity =>
        {
            projectActivity.WorkBreakdownStructure.ProjectActivities ??= [];
            projectActivity.WorkBreakdownStructure.ProjectActivities.Add(projectActivity);
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