using plantool.Presentation.SignalR;
using plantool.Services.Activities;
using plantool.Services.Csv;
using plantool.Services.Engineers;
using plantool.Services.Projects;
using plantool.Services.SeedData;

namespace plantool.Services;

public static class DependencyInjector
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CsvProcessingService>();
        services.AddScoped<CsvSyncService>();
        services.AddSingleton<IFileReader, CsvFileReader>();
        services.AddScoped<ProjectsService>();
        services.AddScoped<ActivitiesService>();
        services.AddScoped<SeederService>();
        services.AddScoped<EngineersService>();
        services.AddScoped<IActivityNotifier, ActivityNotifier>();
    }
}