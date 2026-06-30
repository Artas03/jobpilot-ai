using JobPilotAI.Application.Commands;
using JobPilotAI.Application.DTOs;

namespace JobPilotAI.Infrastructure.Persistence;

public interface IProcessedJobStore
{
    Task SaveProcessedAsync(ProcessJobCommand command, ProcessJobResult result);
}
