using System.Text.Json;
using JobPilotAI.Application.Commands;
using JobPilotAI.Application.DTOs;
using JobPilotAI.Application.Interfaces;
using JobPilotAI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPilotAI.Infrastructure.Persistence;

public sealed class SQLiteJobRepository(JobPilotDbContext dbContext)
    : IJobRepository, IProcessedJobStore
{
    public async Task SaveAsync(Job job)
    {
        ArgumentNullException.ThrowIfNull(job);

        var entity = await dbContext.ProcessedJobs
            .SingleOrDefaultAsync(existing => existing.Id == job.Id);

        if (entity is null)
        {
            entity = new ProcessedJobEntity { Id = job.Id };
            entity.UpdateFrom(job);
            dbContext.ProcessedJobs.Add(entity);
        }
        else
        {
            entity.UpdateFrom(job);
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task SaveProcessedAsync(ProcessJobCommand command, ProcessJobResult result)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(result);

        var entity = await dbContext.ProcessedJobs
            .SingleOrDefaultAsync(existing => existing.Id == result.JobId);

        if (entity is null)
        {
            entity = new ProcessedJobEntity { Id = result.JobId };
            dbContext.ProcessedJobs.Add(entity);
        }

        entity.CreatedAt = DateTime.UtcNow;
        entity.TradeType = command.TradeType;
        entity.CustomerName = command.CustomerName;
        entity.JobAddress = command.JobAddress;
        entity.RawNotes = command.RawNotes;
        entity.LabourHours = command.LabourHours;
        entity.HourlyRate = command.HourlyRate;
        entity.MaterialsCost = command.MaterialsCost;
        entity.ProfessionalSummary = result.ProfessionalSummary;
        entity.FollowUpMessage = result.FollowUpMessage;
        entity.SocialMediaPost = result.SocialMediaPost;
        entity.SuggestedNextActionsJson = JsonSerializer.Serialize(result.SuggestedNextActions);
        entity.InvoiceNumber = result.Invoice.InvoiceNumber;
        entity.InvoiceSubtotal = result.Invoice.Subtotal;
        entity.InvoiceTaxAmount = result.Invoice.TaxAmount;
        entity.InvoiceTotal = result.Invoice.Total;
        entity.InvoiceItemsJson = JsonSerializer.Serialize(result.Invoice.Items.Select(item => new
        {
            item.Id,
            item.Description,
            item.Quantity,
            item.UnitPrice,
            item.Total
        }));

        await dbContext.SaveChangesAsync();
    }

    public async Task<Job?> GetAsync(Guid id)
    {
        var entity = await dbContext.ProcessedJobs
            .AsNoTracking()
            .SingleOrDefaultAsync(job => job.Id == id);

        return entity?.ToDomain();
    }

    public async Task<IEnumerable<Job>> GetAllAsync()
    {
        var entities = await dbContext.ProcessedJobs
            .AsNoTracking()
            .OrderByDescending(job => job.CreatedAt)
            .ToListAsync();

        return entities.Select(entity => entity.ToDomain()).ToArray();
    }
}
