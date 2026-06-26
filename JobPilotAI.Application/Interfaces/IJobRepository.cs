using JobPilotAI.Domain.Models;

namespace JobPilotAI.Application.Interfaces;

public interface IJobRepository
{
    Task SaveAsync(Job job);

    Task<Job?> GetAsync(Guid id);

    Task<IEnumerable<Job>> GetAllAsync();
}
