using System.Text.Json.Serialization;

namespace JobPilotAI.Web.Models;

public class SavedJob
{
    public Guid Id { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string TradeType { get; set; } = string.Empty;

    [JsonPropertyName("location")]
    public string? JobAddress { get; set; }

    [JsonPropertyName("requestedOn")]
    public DateTime CreatedAt { get; set; }

    public decimal? InvoiceTotal { get; set; }
}

public sealed class SavedJobDetails : SavedJob
{
    [JsonPropertyName("description")]
    public string RawNotes { get; set; } = string.Empty;

    public string? ProfessionalSummary { get; set; }

    public string? FollowUpMessage { get; set; }

    public IReadOnlyCollection<string> SuggestedNextActions { get; set; } = [];

    public string? SocialMediaPost { get; set; }

    public InvoiceResult? Invoice { get; set; }
}
