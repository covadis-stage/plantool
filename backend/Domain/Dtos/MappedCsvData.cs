using plantool.Domain.Entities;

namespace plantool.Domain.Dtos;

public class MappedCsvData(List<Project> projects, List<ActivityType> activityTypes, List<WorkCenter> workCenters, List<ProjectActivity> projectActivities)
{
    public List<Project> Projects { get; set; } = projects;
    public List<ActivityType> ActivityTypes { get; set; } = activityTypes;
    public List<WorkCenter> WorkCenters { get; set; } = workCenters;
    public List<ProjectActivity> ProjectActivities { get; set; } = projectActivities;
}