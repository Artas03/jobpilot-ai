using System.Collections.Concurrent;
using JobPilotAI.Application.Interfaces;
using JobPilotAI.Domain.Models;

namespace JobPilotAI.Infrastructure.Persistence;

public class InMemoryJobRepository : IJobRepository
{
    private readonly ConcurrentDictionary<Guid, Job> _jobs = new();

    public Task SaveAsync(Job job)
    {
        ArgumentNullException.ThrowIfNull(job);

        _jobs.AddOrUpdate(job.Id, job, (_, _) => job);

        return Task.CompletedTask;
    }

    public Task<Job?> GetAsync(Guid id)
    {
        _jobs.TryGetValue(id, out var job);

        return Task.FromResult(job);
    }

    public Task<IEnumerable<Job>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Job>>(_jobs.Values.ToArray());
    }
}
