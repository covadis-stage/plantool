using Microsoft.EntityFrameworkCore;
using plantool.Data;
using plantool.Domain.Dtos;
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
        .Where(activity => !activity.IsArchived)
        .Include(activity => activity.ActivityType)
        .Include(activity => activity.WorkCenter)
        .Include(activity => activity.Engineer)
        .Include(activity => activity.Delegator)
        .ToListAsync();

    public async Task BulkUpdate(BulkUpdateRequest request)
    {
        var activities = await _dbContext.Activities
            .Where(activity => request.ActivityKeys.Contains(activity.Key))
            .ToListAsync();

        foreach (var activity in activities)
        {
            if (request.DelegatorId != null)
                await SetDelegator(activity, request.DelegatorId);

            if (request.EngineerId != null)
                await SetEngineer(activity, request.EngineerId);
        }

        await _dbContext.SaveChangesAsync();
    }

    private async Task SetDelegator(ProjectActivity activity, string delegatorId)
    {
        var delegator = await _dbContext.Engineers.FindAsync(delegatorId) ?? throw new InvalidOperationException($"Delegator with ID '{delegatorId}' not found.");
        if (!delegator.CanDelegate) throw new InvalidOperationException($"Delegator with ID '{delegatorId}' does not have delegation rights.");
        
        activity.DelegatorId = delegatorId;
        activity.Delegator = delegator;

        _dbContext.Entry(activity).State = EntityState.Modified;
    }

    private async Task SetEngineer(ProjectActivity activity, string engineerId)
    {
        var engineer = await _dbContext.Engineers.FindAsync(engineerId) ?? throw new InvalidOperationException($"Engineer with ID '{engineerId}' not found.");
        
        activity.EngineerId = engineerId;
        activity.Engineer = engineer;

        _dbContext.Entry(activity).State = EntityState.Modified;
    }
}