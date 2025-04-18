using Microsoft.EntityFrameworkCore;
using plantool.Data;
using plantool.Domain.Entities;

namespace plantool.Services.Projects;

public class ProjectsService
{
    private readonly PlantoolDbContext _dbContext;
    private readonly ILogger<ProjectsService> _logger;

    public ProjectsService(PlantoolDbContext dbContext, ILogger<ProjectsService> logger) =>
        (_dbContext, _logger) = (dbContext, logger);

    public async Task<List<Project>> GetProjectsAsync() => await _dbContext.Projects.ToListAsync();
}