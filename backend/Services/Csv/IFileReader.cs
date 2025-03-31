namespace plantool.Services.Csv;

public interface IFileReader
{
    Task<(List<string> columnHeaders, List<string[]> rows)> ReadAllLinesAsync();
}