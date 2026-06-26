using JobPilotAI.Domain.Enums;

namespace JobPilotAI.Domain.Models;

public class Job
{
    public Job(
        string customerName,
        string description,
        TradeType tradeType,
        DateTime requestedOn,
        string? customerEmail = null,
        string? customerPhone = null,
        string? location = null,
        DateTime? scheduledFor = null,
        Guid? id = null)
    {
        if (string.IsNullOrWhiteSpace(customerName))
        {
            throw new ArgumentException("Customer name is required.", nameof(customerName));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Job description is required.", nameof(description));
        }

        Id = id ?? Guid.NewGuid();
        CustomerName = customerName.Trim();
        Description = description.Trim();
        TradeType = tradeType;
        RequestedOn = requestedOn;
        CustomerEmail = string.IsNullOrWhiteSpace(customerEmail) ? null : customerEmail.Trim();
        CustomerPhone = string.IsNullOrWhiteSpace(customerPhone) ? null : customerPhone.Trim();
        Location = string.IsNullOrWhiteSpace(location) ? null : location.Trim();
        ScheduledFor = scheduledFor;
    }

    public Guid Id { get; private set; }

    public string CustomerName { get; private set; }

    public string? CustomerEmail { get; private set; }

    public string? CustomerPhone { get; private set; }

    public string? Location { get; private set; }

    public string Description { get; private set; }

    public TradeType TradeType { get; private set; }

    public DateTime RequestedOn { get; private set; }

    public DateTime? ScheduledFor { get; private set; }

    public void Schedule(DateTime scheduledFor)
    {
        ScheduledFor = scheduledFor;
    }
}
