using Microsoft.EntityFrameworkCore;
using plantool.Data;
using plantool.Domain.Entities;

namespace plantool.Services.Activities;

public class ActivitiesService
{
    private readonly PlantoolDbContext _dbContext;
    private readonly ILogger<ActivitiesService> _logger;

    public ActivitiesService(PlantoolDbContext dbContext, ILogger<ActivitiesService> logger) =>
        (_dbContext, _logger) = (dbContext, logger);

    public async Task<List<ProjectActivity>> GetActivitiesByProjectKeyAsync(string projectKey) =>
        await _dbContext.Activities
        .Where(activity => activity.ProjectId == projectKey)
        .Include(activity => activity.ActivityType)
        .ToListAsync();
}