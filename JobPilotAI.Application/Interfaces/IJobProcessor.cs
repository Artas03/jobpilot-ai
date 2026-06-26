using JobPilotAI.Application.Commands;
using JobPilotAI.Application.DTOs;

namespace JobPilotAI.Application.Interfaces;

public interface IJobProcessor
{
    Task<ProcessJobResult> ProcessAsync(ProcessJobCommand command);
}
