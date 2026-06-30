using System.Text.Json.Serialization;

namespace JobPilotAI.Web.Models;

public sealed class SavedJob
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
