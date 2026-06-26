using JobPilotAI.Application.Commands;
using JobPilotAI.Application.DTOs;
using JobPilotAI.Application.Interfaces;
using JobPilotAI.Domain.Enums;
using JobPilotAI.Domain.Models;

namespace JobPilotAI.Application.Services;

public class JobProcessor : IJobProcessor
{
    public Task<ProcessJobResult> ProcessAsync(ProcessJobCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        Validate(command);

        var job = new Job(
            command.CustomerName,
            command.RawNotes,
            command.TradeType,
            DateTime.UtcNow,
            location: command.JobAddress);

        var invoice = CreatePlaceholderInvoice(job, command);

        var result = new ProcessJobResult(
            job.Id,
            CreatePlaceholderProfessionalSummary(job),
            invoice,
            CreatePlaceholderFollowUpMessage(job),
            CreatePlaceholderSuggestedNextActions(),
            CreatePlaceholderSocialMediaPost(job));

        return Task.FromResult(result);
    }

    private static void Validate(ProcessJobCommand command)
    {
        if (command.TradeType == TradeType.Unknown)
        {
            throw new ArgumentException("Trade type is required.", nameof(command));
        }

        if (string.IsNullOrWhiteSpace(command.CustomerName))
        {
            throw new ArgumentException("Customer name is required.", nameof(command));
        }

        if (string.IsNullOrWhiteSpace(command.JobAddress))
        {
            throw new ArgumentException("Job address is required.", nameof(command));
        }

        if (string.IsNullOrWhiteSpace(command.RawNotes))
        {
            throw new ArgumentException("Raw notes are required.", nameof(command));
        }

        if (command.LabourHours < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(command), "Labour hours cannot be negative.");
        }

        if (command.HourlyRate < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(command), "Hourly rate cannot be negative.");
        }

        if (command.MaterialsCost < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(command), "Materials cost cannot be negative.");
        }
    }

    private static Invoice CreatePlaceholderInvoice(Job job, ProcessJobCommand command)
    {
        var invoice = new Invoice(
            job.Id,
            $"DRAFT-{job.Id:N}"[..14],
            DateTime.UtcNow);

        if (command.LabourHours > 0 && command.HourlyRate > 0)
        {
            invoice.AddItem(new InvoiceItem("Labour", command.LabourHours, command.HourlyRate));
        }

        if (command.MaterialsCost > 0)
        {
            invoice.AddItem(new InvoiceItem("Materials", 1, command.MaterialsCost));
        }

        return invoice;
    }

    private static string CreatePlaceholderProfessionalSummary(Job job)
    {
        return $"Placeholder summary for {job.TradeType} job at {job.Location}.";
    }

    private static string CreatePlaceholderFollowUpMessage(Job job)
    {
        return $"Placeholder follow-up message for {job.CustomerName}.";
    }

    private static IReadOnlyCollection<string> CreatePlaceholderSuggestedNextActions()
    {
        return
        [
            "Review job notes",
            "Confirm customer availability",
            "Prepare final invoice"
        ];
    }

    private static string CreatePlaceholderSocialMediaPost(Job job)
    {
        return $"Placeholder social media post for a completed {job.TradeType} job.";
    }
}
