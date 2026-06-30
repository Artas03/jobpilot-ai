using JobPilotAI.Application.Commands;
using JobPilotAI.Application.DTOs;
using JobPilotAI.Application.Interfaces;
using JobPilotAI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace JobPilotAI.Api.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobController : ControllerBase
{
    private readonly IJobProcessor _jobProcessor;
    private readonly IProcessedJobStore _processedJobStore;

    public JobController(IJobProcessor jobProcessor, IProcessedJobStore processedJobStore)
    {
        _jobProcessor = jobProcessor;
        _processedJobStore = processedJobStore;
    }

    [HttpPost("process")]
    [ProducesResponseType(typeof(ProcessJobResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProcessJobResult>> Process(ProcessJobCommand command)
    {
        var result = await _jobProcessor.ProcessAsync(command);
        await _processedJobStore.SaveProcessedAsync(command, result);

        return Ok(result);
    }

    [HttpGet("health")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Health()
    {
        return Ok(new
        {
            status = "Healthy",
            version = "0.1.0"
        });
    }
}
