namespace plantool.Services.CsvService;

public interface IFileReader
{
    Task<(List<string> columnHeaders, List<string[]> rows)> ReadAllLinesAsync();
}