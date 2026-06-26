using JobPilotAI.Domain.Models;

namespace JobPilotAI.Application.Interfaces;

public interface IAiJobAssistant
{
    string GenerateProfessionalSummary(Job job);

    string GenerateFollowUpMessage(Job job);

    string GenerateSocialMediaPost(Job job);

    IEnumerable<string> GenerateSuggestedActions(Job job);
}
