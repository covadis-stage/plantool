namespace plantool.Services.CsvService;

public class CsvProcessingService
{
    private readonly ILogger<CsvProcessingService> _logger;
    private readonly CsvSyncService _csvSyncService;
    private readonly IFileReader _fileReader;

    public CsvProcessingService(ILogger<CsvProcessingService> logger, CsvSyncService csvSyncService, IFileReader fileReader) 
        => (_logger, _csvSyncService, _fileReader) = (logger, csvSyncService, fileReader);

    public async Task ProcessCsv()
    {
        _logger.LogInformation("Reading CSV file...");
        var (columnHeaders, rows) = await _fileReader.ReadAllLinesAsync();

        _logger.LogInformation("CSV file read, processing...");
        var mappedCsv = CsvMappingService.MapCsv(columnHeaders, rows);

        _logger.LogInformation("CSV file processed, syncing changes to database...");
        await _csvSyncService.SyncCsvDataAsync(mappedCsv);

        _logger.LogInformation("Changes saved to database.");
    }
}