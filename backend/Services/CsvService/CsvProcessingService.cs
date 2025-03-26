namespace plantool.Services.CsvService;

public class CsvProcessingService
{
    private readonly ILogger<CsvProcessingService> _logger;
    private readonly CsvSyncService _csvSyncService;

    public CsvProcessingService(ILogger<CsvProcessingService> logger, CsvSyncService csvSyncService) 
        => (_logger, _csvSyncService) = (logger, csvSyncService);

    public async Task ProcessCsv()
    {
        _logger.LogInformation("Reading CSV file...");
        var csvFile = Path.Combine(Directory.GetCurrentDirectory(), "InputData", "Engineering Orderplanning.csv");
        var csvData = await File.ReadAllLinesAsync(csvFile);
        var columns = csvData[0].Split(';').ToList();
        var csvRows = csvData.Skip(1).Select(row => row.Split(';')).ToList();

        _logger.LogInformation("CSV file read, processing...");
        var mappedCsv = CsvMappingService.MapCsv(columns, csvRows);

        _logger.LogInformation("CSV file processed, saving to database...");
        await _csvSyncService.SyncCsvDataAsync(mappedCsv);

        _logger.LogInformation("Changes saved to database.");
    }
}