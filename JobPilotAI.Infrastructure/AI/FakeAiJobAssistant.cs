using JobPilotAI.Application.Interfaces;
using JobPilotAI.Domain.Models;

namespace JobPilotAI.Infrastructure.AI;

public class FakeAiJobAssistant : IAiJobAssistant
{
    public string GenerateProfessionalSummary(Job job)
    {
        ArgumentNullException.ThrowIfNull(job);

        var location = FormatLocation(job);

        return $"{job.TradeType} job for {job.CustomerName} at {location}. "
            + $"Customer notes: {job.Description}. Recommended next step is to confirm the scope and schedule the work.";
    }

    public string GenerateFollowUpMessage(Job job)
    {
        ArgumentNullException.ThrowIfNull(job);

        return $"Hi {job.CustomerName}, thanks for sharing the details of your {job.TradeType.ToString().ToLowerInvariant()} job. "
            + "We have reviewed the notes and can confirm the next steps, timing, and estimated costs with you shortly.";
    }

    public string GenerateSocialMediaPost(Job job)
    {
        ArgumentNullException.ThrowIfNull(job);

        return $"Another {job.TradeType.ToString().ToLowerInvariant()} job reviewed and ready to schedule. "
            + "Clear notes, transparent pricing, and a professional follow-up make every job easier to manage.";
    }

    public IEnumerable<string> GenerateSuggestedActions(Job job)
    {
        ArgumentNullException.ThrowIfNull(job);

        var actions = new List<string>
        {
            $"Confirm the {job.TradeType.ToString().ToLowerInvariant()} job scope with {job.CustomerName}.",
            $"Check access details for {FormatLocation(job)}.",
            "Prepare labour and materials estimate."
        };

        if (job.ScheduledFor.HasValue)
        {
            actions.Add($"Confirm appointment time for {job.ScheduledFor.Value:g}.");
        }
        else
        {
            actions.Add("Offer the customer available appointment slots.");
        }

        return actions;
    }

    private static string FormatLocation(Job job)
    {
        return string.IsNullOrWhiteSpace(job.Location)
            ? "the customer address"
            : job.Location;
    }
}
