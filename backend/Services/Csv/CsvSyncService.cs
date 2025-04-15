using Microsoft.EntityFrameworkCore;
using plantool.Data;
using plantool.Domain.Dtos;
using plantool.Domain.Entities;

namespace plantool.Services.Csv;

public class CsvSyncService(PlantoolDbContext context, ILogger<CsvSyncService> logger)
{
    private readonly PlantoolDbContext _context = context;
    private readonly ILogger<CsvSyncService> _logger = logger;

    public async Task SyncCsvDataAsync(MappedCsvData csvData)
    {
        csvData.Projects.ForEach(p => p.Activities = []);
        csvData.ProjectActivities.ForEach(pa => pa.ActivityType = null!);
        csvData.ProjectActivities.ForEach(pa => pa.Project = null!);
        csvData.ProjectActivities.ForEach(pa => pa.WorkCenter = null!);

        var existingActivityTypes = await _context.ActivityTypes.ToListAsync() ?? [];
        SyncAuditable(existingActivityTypes.Cast<IAuditable>().ToList(), csvData.ActivityTypes.Cast<IAuditable>().ToList());

        var existingWorkCenters = await _context.WorkCenters.ToListAsync() ?? [];
        SyncAuditable(existingWorkCenters.Cast<IAuditable>().ToList(), csvData.WorkCenters.Cast<IAuditable>().ToList());

        var existingProjects = await _context.Projects.ToListAsync() ?? [];
        SyncAuditable(existingProjects.Cast<IAuditable>().ToList(), csvData.Projects.Cast<IAuditable>().ToList());

        var existingActivities = await _context.Activities.ToListAsync() ?? [];
        SyncAuditable(existingActivities.Cast<IAuditable>().ToList(), csvData.ProjectActivities.Cast<IAuditable>().ToList());

        await _context.SaveChangesAsync();
    }

    private void SyncAuditable(List<IAuditable> existing, List<IAuditable> imported)
    {
        var auditableComparer = new AuditableComparer();

        var toArchive = existing.Where(a => !a.IsArchived).Except(imported, auditableComparer).ToList();
        var toCreate = imported.Except(existing, auditableComparer).ToList();
        var toUpdate = imported.Intersect(existing, auditableComparer).ToList();

        foreach (var entity in toArchive) entity.IsArchived = true;

        foreach (var entity in toCreate) _context.Add(entity);

        foreach (var entity in toUpdate)
        {
            var existingEntity = existing.First(e => e.Key == entity.Key);
            UpdateEntity(existingEntity, entity);
        }

        _logger.LogInformation(
            "\n----------------------------------------\n" +
            $"Archiving {toArchive.Count} of type {imported.First().GetType().Name}...\n" +
            $"Creating {toCreate.Count} of type {imported.First().GetType().Name}...\n" +
            $"Updating {toUpdate.Count} of type {imported.First().GetType().Name}..." +
            "\n----------------------------------------"
        );
    }

    private void UpdateEntity(IAuditable existingEntity, IAuditable importedEntity)
    {
        if (existingEntity is ProjectActivity existingActivity && importedEntity is ProjectActivity importedActivity)
            UpdateActivity(existingActivity, importedActivity);
        else
            _context.Entry(existingEntity).CurrentValues.SetValues(importedEntity);
    }

    private void UpdateActivity(ProjectActivity existing, ProjectActivity imported)
    {
        existing.Key = imported.Key;
        existing.Id = imported.Id;
        existing.LatestStartDate = imported.LatestStartDate;
        existing.LatestFinishDate = imported.LatestFinishDate;
        existing.OriginalFinishDate = imported.OriginalFinishDate;
        existing.TimeEstimated = imported.TimeEstimated;
        existing.TimeSpent = imported.TimeSpent;
        existing.TeamLeader = imported.TeamLeader;
        existing.WorkCenter = imported.WorkCenter;
        existing.ActivityTypeCode = imported.ActivityTypeCode;
        existing.WorkBreakdownStructure = imported.WorkBreakdownStructure;
        existing.Network = imported.Network;
        existing.ProjectId = imported.ProjectId;
        existing.IsArchived = false;

        _context.Entry(existing).State = EntityState.Modified;
    }
}