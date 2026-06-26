using JobPilotAI.Domain.Models;

namespace JobPilotAI.Application.DTOs;

public class ProcessJobResult
{
    public ProcessJobResult(
        Guid jobId,
        string professionalSummary,
        Invoice invoice,
        string followUpMessage,
        IEnumerable<string> suggestedNextActions,
        string socialMediaPost)
    {
        if (jobId == Guid.Empty)
        {
            throw new ArgumentException("Job id is required.", nameof(jobId));
        }

        ArgumentNullException.ThrowIfNull(invoice);

        JobId = jobId;
        ProfessionalSummary = professionalSummary;
        Invoice = invoice;
        FollowUpMessage = followUpMessage;
        SuggestedNextActions = suggestedNextActions.ToArray();
        SocialMediaPost = socialMediaPost;
    }

    public Guid JobId { get; }

    public string ProfessionalSummary { get; }

    public Invoice Invoice { get; }

    public string FollowUpMessage { get; }

    public IReadOnlyCollection<string> SuggestedNextActions { get; }

    public string SocialMediaPost { get; }
}
