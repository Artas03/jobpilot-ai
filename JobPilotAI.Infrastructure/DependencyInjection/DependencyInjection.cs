using JobPilotAI.Application.Interfaces;
using JobPilotAI.Infrastructure.AI;
using JobPilotAI.Infrastructure.Persistence;
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
        services.AddSingleton<IJobRepository, InMemoryJobRepository>();

        return services;
    }
}
