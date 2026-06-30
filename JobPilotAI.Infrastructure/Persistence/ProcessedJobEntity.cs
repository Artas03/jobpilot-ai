using JobPilotAI.Domain.Enums;
using JobPilotAI.Domain.Models;

namespace JobPilotAI.Infrastructure.Persistence;

public sealed class ProcessedJobEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public TradeType TradeType { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string? CustomerEmail { get; set; }

    public string? CustomerPhone { get; set; }

    public string? JobAddress { get; set; }

    public string RawNotes { get; set; } = string.Empty;

    public DateTime? ScheduledFor { get; set; }

    public decimal LabourHours { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal MaterialsCost { get; set; }

    public string? ProfessionalSummary { get; set; }

    public string? FollowUpMessage { get; set; }

    public string? SocialMediaPost { get; set; }

    public string SuggestedNextActionsJson { get; set; } = "[]";

    public decimal? InvoiceSubtotal { get; set; }

    public string? InvoiceNumber { get; set; }

    public decimal? InvoiceTaxAmount { get; set; }

    public decimal? InvoiceTotal { get; set; }

    public string InvoiceItemsJson { get; set; } = "[]";

    public void UpdateFrom(Job job)
    {
        CreatedAt = job.RequestedOn;
        TradeType = job.TradeType;
        CustomerName = job.CustomerName;
        CustomerEmail = job.CustomerEmail;
        CustomerPhone = job.CustomerPhone;
        JobAddress = job.Location;
        RawNotes = job.Description;
        ScheduledFor = job.ScheduledFor;
    }

    public Job ToDomain()
    {
        return new Job(
            CustomerName,
            RawNotes,
            TradeType,
            CreatedAt,
            CustomerEmail,
            CustomerPhone,
            JobAddress,
            ScheduledFor,
            Id);
    }
}
