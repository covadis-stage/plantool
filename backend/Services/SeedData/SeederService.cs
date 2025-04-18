using Microsoft.EntityFrameworkCore;
using plantool.Data;
using plantool.Domain.Entities;

namespace plantool.Services.SeedData;

public class SeederService(PlantoolDbContext dbContext, ILogger<SeederService> logger)
{
    private readonly PlantoolDbContext _dbContext = dbContext;
    private readonly ILogger<SeederService> _logger = logger;

    public async Task<string> SeedTeamsAndEngineersAsync(bool force)
    {
        // Start with an empty database
        if (await _dbContext.CompetenceTeams.AnyAsync() || await _dbContext.Engineers.AnyAsync() || await _dbContext.ProductTeams.AnyAsync())
        {
            if (!force) return "Teams and engineers already exist. Use force=true to override.";
            
            _logger.LogInformation("Force seeding teams and engineers. Existing data will be overridden.");

            foreach(var productTeam in await _dbContext.ProductTeams.ToListAsync())
            {
                productTeam.ProductLead = null;
                productTeam.ProductLeadId = null;
            }
            await _dbContext.SaveChangesAsync();

            _dbContext.Engineers.RemoveRange(await _dbContext.Engineers.ToListAsync());
            _dbContext.ProductTeams.RemoveRange(await _dbContext.ProductTeams.ToListAsync());
            _dbContext.CompetenceTeams.RemoveRange(await _dbContext.CompetenceTeams.ToListAsync());
        }

        // Add competence teams
        string EngMechRad = "Eng mech RAD";
        string HardwareRad = "Hardware RAD";
        string SoftwareRad = "Software RAD";
        var competenceTeams = new List<CompetenceTeam>
        {
            new() { Name = EngMechRad },
            new() { Name = HardwareRad },
            new() { Name = SoftwareRad },
        };
        _dbContext.CompetenceTeams.AddRange(competenceTeams);

        // Add product teams and link them to competence teams
#pragma warning disable S1192
        var productTeams = new List<ProductTeam>
        {
            new() { Name = "Pastry Mechanical", CompetenceTeamName = EngMechRad },
            new() { Name = "Bread Hardware", CompetenceTeamName = HardwareRad },
            new() { Name = "Pie and Deco Software", CompetenceTeamName = SoftwareRad },
            new() { Name = "Pastry Software", CompetenceTeamName = SoftwareRad },
            new() { Name = "Bread Mechanical", CompetenceTeamName = EngMechRad },
            new() { Name = "Pie and Deco Hardware", CompetenceTeamName = HardwareRad },
            new() { Name = "Cake Mechanical", CompetenceTeamName = EngMechRad },
            new() { Name = "Cookie", CompetenceTeamName = HardwareRad },
            new() { Name = "Muffin_S", CompetenceTeamName = SoftwareRad },
            new() { Name = "Donut Mechanical", CompetenceTeamName = EngMechRad },
            new() { Name = "Brownie", CompetenceTeamName = HardwareRad },
            new() { Name = "Cupcake", CompetenceTeamName = SoftwareRad },
            new() { Name = "Tart Mechanical", CompetenceTeamName = EngMechRad },
            new() { Name = "Bagel Hardware", CompetenceTeamName = HardwareRad },
            new() { Name = "Croissant", CompetenceTeamName = SoftwareRad },
            new() { Name = "Eclair Mechanical", CompetenceTeamName = "Eng mech RAD" },
        };
#pragma warning restore S1192
        foreach (var productTeam in productTeams)
        {
            var competenceTeam = competenceTeams.FirstOrDefault(ct => ct.Name == productTeam.CompetenceTeamName);
            if (competenceTeam != null)
            {
                productTeam.CompetenceTeam = competenceTeam;
                competenceTeam.ProductTeams.Add(productTeam);
            }
        }
        _dbContext.ProductTeams.AddRange(productTeams);

        // Add engineers and link them to product teams
        var engineers = new List<Engineer>
        {
            new() { Name = "Alice Marie Johnson", ProductTeamName = "Pastry Mechanical" },
            new() { Name = "Bob Alexander Smith", ProductTeamName = "Bread Hardware" },
            new() { Name = "Charlie Edward Brown", ProductTeamName = "Pie and Deco Software" },
            new() { Name = "David Christopher Wilson", ProductTeamName = "Pastry Software" },
            new() { Name = "Eve Patricia Davis", ProductTeamName = "Bread Mechanical" },
            new() { Name = "Frank Miller", ProductTeamName = "Pie and Deco Hardware" },
            new() { Name = "Grace Lee", ProductTeamName = "Cake Mechanical" },
            new() { Name = "Heidi Clark", ProductTeamName = "Cookie" },
            new() { Name = "Ivan Moore", ProductTeamName = "Muffin_S" },
            new() { Name = "Judy Taylor", ProductTeamName = "Donut Mechanical" },
            new() { Name = "Mallory Anderson", ProductTeamName = "Brownie" },
            new() { Name = "Niaj Thomas", ProductTeamName = "Cupcake" },
            new() { Name = "Olivia Harris", ProductTeamName = "Tart Mechanical" },
            new() { Name = "Peggy Martin", ProductTeamName = "Bagel Hardware" },
            new() { Name = "Rupert White", ProductTeamName = "Croissant" },
            new() { Name = "Sybil Hall", ProductTeamName = "Eclair Mechanical" },
            new() { Name = "Trent Baker", ProductTeamName = "Pastry Mechanical" },
            new() { Name = "Uma Carter", ProductTeamName = "Bread Hardware" },
            new() { Name = "Victor Evans", ProductTeamName = "Pie and Deco Software" },
            new() { Name = "Wendy Foster", ProductTeamName = "Pastry Software" },
            new() { Name = "Xander Green", ProductTeamName = "Bread Mechanical" },
            new() { Name = "Yvonne Hughes", ProductTeamName = "Pie and Deco Hardware", CanDelegate = true },
            new() { Name = "Zachary Jenkins", ProductTeamName = "Cake Mechanical", CanDelegate = true },
            new() { Name = "Abigail King", ProductTeamName = "Cookie", CanDelegate = true },
            new() { Name = "Benjamin Lewis", ProductTeamName = "Muffin_S", CanDelegate = true },
            new() { Name = "Charlotte Morgan", ProductTeamName = "Donut Mechanical", CanDelegate = true },
            new() { Name = "Daniel Nelson", ProductTeamName = "Pastry Mechanical", CanDelegate = true },
            new() { Name = "Ella Owens", ProductTeamName = "Pastry Mechanical", CanDelegate = true },
            new() { Name = "Finn Parker", ProductTeamName = "Pastry Software", CanDelegate = true },
            new() { Name = "Grace Quinn", ProductTeamName = "Bagel Hardware", CanDelegate = true },
            new() { Name = "Henry Roberts", ProductTeamName = "Croissant", CanDelegate = true },
            new() { Name = "Isla Scott", ProductTeamName = "Eclair Mechanical", CanDelegate = true },
            new() { Name = "Jack Turner", ProductTeamName = "Pastry Mechanical", CanDelegate = true },
            new() { Name = "Kara Underwood", ProductTeamName = "Pastry Software", CanDelegate = true },
            new() { Name = "Liam Vaughn", ProductTeamName = "Pie and Deco Software", CanDelegate = true },
            new() { Name = "Mia Walker", ProductTeamName = "Pastry Software", CanDelegate = true },
        };
        foreach (var engineer in engineers)
        {
            var productTeam = productTeams.FirstOrDefault(pt => pt.Name == engineer.ProductTeamName);
            if (productTeam != null)
            {
                engineer.ProductTeam = productTeam;
                productTeam.Engineers.Add(engineer);
            }
        }
        _dbContext.Engineers.AddRange(engineers);

        // Assign product leads to product teams
        for(int i = 0; i < productTeams.Count; i++)
        {
            var engineer = engineers[Random.Shared.Next(engineers.Count)];
            productTeams[i].ProductLead = engineer;
            productTeams[i].ProductLeadId = engineer.Id;
        }

        _dbContext.ProductTeams.UpdateRange(productTeams);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Teams and engineers seeded successfully.");
        return "Teams and engineers seeded successfully.";
    }
}