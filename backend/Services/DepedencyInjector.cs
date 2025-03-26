using plantool.Services.CsvService;

namespace plantool.Services;

public static class DependencyInjector
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CsvProcessingService>();
        services.AddScoped<CsvSyncService>();
        services.AddSingleton<IFileReader, CsvFileReader>();
    }
}