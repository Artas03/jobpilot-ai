using JobPilotAI.Application.Interfaces;
using JobPilotAI.Infrastructure.AI;
using JobPilotAI.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace JobPilotAI.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<IAiJobAssistant, FakeAiJobAssistant>();
        services.AddSingleton<IJobRepository, InMemoryJobRepository>();

        return services;
    }
}
