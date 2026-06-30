using JobPilotAI.Application.Commands;
using JobPilotAI.Application.DTOs;
using JobPilotAI.Application.Interfaces;
using JobPilotAI.Domain.Models;
using JobPilotAI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace JobPilotAI.Api.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobController : ControllerBase
{
    private readonly IJobProcessor _jobProcessor;
    private readonly IJobRepository _jobRepository;
    private readonly IProcessedJobStore _processedJobStore;

    public JobController(
        IJobProcessor jobProcessor,
        IJobRepository jobRepository,
        IProcessedJobStore processedJobStore)
    {
        _jobProcessor = jobProcessor;
        _jobRepository = jobRepository;
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

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Job>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Job>>> GetAll()
    {
        var jobs = await _jobRepository.GetAllAsync();
        var newestFirst = jobs.OrderByDescending(job => job.RequestedOn);

        return Ok(newestFirst);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Job), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Job>> Get(Guid id)
    {
        var job = await _jobRepository.GetAsync(id);

        return job is null ? NotFound() : Ok(job);
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
