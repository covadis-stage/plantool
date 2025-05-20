using Microsoft.EntityFrameworkCore;
using plantool.Data;
using plantool.Domain.Dtos;
using plantool.Domain.Entities;

namespace plantool.Services.Activities;

public class ActivitiesService
{
    private readonly PlantoolDbContext _dbContext;
    private readonly ILogger<ActivitiesService> _logger;
    private readonly IActivityNotifier _activityNotifier;

    public ActivitiesService(PlantoolDbContext dbContext, ILogger<ActivitiesService> logger, IActivityNotifier activityNotifier) =>
        (_dbContext, _logger, _activityNotifier) = (dbContext, logger, activityNotifier);

    public async Task<List<ProjectActivity>> GetActivitiesByProjectKeyAsync(string projectKey) =>
        await _dbContext.Activities
        .Where(activity => activity.ProjectId == projectKey)
        .Where(activity => !activity.IsArchived)
        .Include(activity => activity.ActivityType)
        .Include(activity => activity.WorkCenter)
        .Include(activity => activity.Engineer)
        .Include(activity => activity.Delegator)
        .ToListAsync();


// will this delete remarks when null?
    public async Task<bool> SetGeneralRemark(string activityKey, string? remark)
    {
        var activity = await _dbContext.Activities.FindAsync(activityKey) ?? throw new InvalidOperationException($"Activity with key '{activityKey}' not found.");
        activity.GeneralRemark = remark;
        _dbContext.Entry(activity).State = EntityState.Modified;
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task BulkUpdate(BulkUpdateRequest request)
    {
        var activities = await _dbContext.Activities
            .Where(activity => request.ActivityKeys.Contains(activity.Key))
            .ToListAsync();

        var changedProperties = request.FindProperties();

        foreach (var activity in activities)
        {
            if (request.DelegatorId != null)
                await SetDelegator(activity, request.DelegatorId);

            if (request.EngineerId != null)
                await SetEngineer(activity, request.EngineerId);

            if (request.ActualFinishDate != null)
                activity.ActualFinishDate = request.ActualFinishDate.Value;

            if (request.ActualStartDate != null)
                activity.ActualStartDate = request.ActualStartDate.Value;

            foreach (var property in changedProperties)
            {
                await _activityNotifier.NotifyActivityUpdate(activity.Key, property.Key, property.Value);
            }
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task BulkDelete(BulkDeleteRequest request)
    {
        var activities = await _dbContext.Activities
            .Where(activity => request.ActivityKeys.Contains(activity.Key))
            .ToListAsync();

        var changedProperties = request.FindProperties();

        foreach (var activity in activities)
        {
            if (request.Delegator == true)
                activity.DelegatorId = null;

            if (request.Engineer == true)
                activity.EngineerId = null;

            if (request.ActualFinishDate == true)
                activity.ActualFinishDate = null;

            if (request.ActualStartDate == true)
                activity.ActualStartDate = null;

            foreach (var property in changedProperties)
            {
                await _activityNotifier.NotifyActivityUpdate(activity.Key, property, null);
            }
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