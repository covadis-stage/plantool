namespace plantool.Services.Csv;

public class CsvFileReader : IFileReader
{
    public async Task<(List<string> columnHeaders, List<string[]> rows)> ReadAllLinesAsync()
    {
        var csvFile = Path.Combine(Directory.GetCurrentDirectory(), "InputData", "Engineering Orderplanning.csv");
        var csvData = await File.ReadAllLinesAsync(csvFile);
        var columnHeaders = csvData[0].Split(';').ToList();
        var csvRows = csvData.Skip(1).Select(row => row.Split(';')).ToList();
        return (columnHeaders, csvRows);
    }
}