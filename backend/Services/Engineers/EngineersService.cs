
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using plantool.Data;
using plantool.Domain.Dtos;
using plantool.Domain.Entities;

namespace plantool.Services.Engineers;
public class EngineersService
{
    private readonly ILogger<EngineersService> _logger;
    private readonly PlantoolDbContext _dbContext;

    public EngineersService(PlantoolDbContext dbContext, ILogger<EngineersService> logger) =>
        (_dbContext, _logger) = (dbContext, logger);

    public async Task<List<EngineerDto>> GetAllEngineersAsync()
    {
        var engineers = await _dbContext.Engineers
            .Include(engineer => engineer.ProductTeam)
            .Include(engineer => engineer.ProductTeam.CompetenceTeam)
            .Select(engineer => new EngineerDto(){
                Id = engineer.Id,
                Name = engineer.Name,
                CanDelegate = engineer.CanDelegate
            })
            .ToListAsync();

        return engineers;
    }

    public async Task<List<EngineerDto>> GetAllDelegatorsAsync()
    {
        var delegators = await _dbContext.Engineers
            .Where(engineer => engineer.CanDelegate)
            .Include(engineer => engineer.ProductTeam)
            .Include(engineer => engineer.ProductTeam.CompetenceTeam)
            .Select(engineer => new EngineerDto(){
                Id = engineer.Id,
                Name = engineer.Name,
                CanDelegate = engineer.CanDelegate
            })
            .ToListAsync();

        return delegators;
    }
}
