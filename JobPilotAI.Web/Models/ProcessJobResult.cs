namespace JobPilotAI.Web.Models;

public sealed class ProcessJobResult
{
    public Guid JobId { get; set; }

    public string ProfessionalSummary { get; set; } = string.Empty;

    public InvoiceResult Invoice { get; set; } = new();

    public string FollowUpMessage { get; set; } = string.Empty;

    public IReadOnlyCollection<string> SuggestedNextActions { get; set; } = [];

    public string SocialMediaPost { get; set; } = string.Empty;
}

public sealed class InvoiceResult
{
    public string InvoiceNumber { get; set; } = string.Empty;

    public IReadOnlyCollection<InvoiceItemResult> Items { get; set; } = [];

    public decimal Subtotal { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal Total { get; set; }
}

public sealed class InvoiceItemResult
{
    public string Description { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Total { get; set; }
}
