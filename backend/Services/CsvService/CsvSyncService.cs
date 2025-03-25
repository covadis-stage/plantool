using plantool.Data;
using plantool.Domain;

namespace plantool.Services.CsvService;

public class CsvSyncService(PlantoolDbContext context, ILogger<CsvSyncService> logger)
{
    private readonly PlantoolDbContext _context = context;
    private readonly ILogger<CsvSyncService> _logger = logger;

    public async Task SyncCsvDataAsync(MappedCsvData csvData)
    {
        await _context.SaveChangesAsync();
    }
}