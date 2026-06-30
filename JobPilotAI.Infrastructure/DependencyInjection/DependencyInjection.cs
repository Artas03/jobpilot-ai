using JobPilotAI.Application.Interfaces;
using JobPilotAI.Infrastructure.AI;
using JobPilotAI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobPilotAI.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<IAiJobAssistant>(serviceProvider =>
        {
            var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            return string.IsNullOrWhiteSpace(apiKey)
                ? new FakeAiJobAssistant()
                : new OpenAiJobAssistant(serviceProvider.GetRequiredService<IConfiguration>());
        });
        services.AddDbContext<JobPilotDbContext>((serviceProvider, options) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("JobPilotDb")
                ?? "Data Source=jobpilot.db";

            options.UseSqlite(connectionString);
        });
        services.AddScoped<SQLiteJobRepository>();
        services.AddScoped<IJobRepository>(serviceProvider =>
            serviceProvider.GetRequiredService<SQLiteJobRepository>());
        services.AddScoped<IProcessedJobStore>(serviceProvider =>
            serviceProvider.GetRequiredService<SQLiteJobRepository>());

        return services;
    }

    public static async Task EnsureJobPilotDatabaseCreatedAsync(this IServiceProvider services)
    {
        ArgumentNullException.ThrowIfNull(services);

        await using var scope = services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<JobPilotDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
    }
}
