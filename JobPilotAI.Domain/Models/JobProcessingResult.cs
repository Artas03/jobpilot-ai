using JobPilotAI.Domain.Enums;

namespace JobPilotAI.Domain.Models;

public class JobProcessingResult
{
    public JobProcessingResult(
        Guid jobId,
        bool isSuccessful,
        string summary,
        DateTime processedOn,
        TradeType detectedTradeType = TradeType.Unknown,
        decimal? confidenceScore = null,
        string? errorMessage = null,
        Guid? id = null)
    {
        if (jobId == Guid.Empty)
        {
            throw new ArgumentException("Job id is required.", nameof(jobId));
        }

        if (string.IsNullOrWhiteSpace(summary))
        {
            throw new ArgumentException("Processing summary is required.", nameof(summary));
        }

        if (confidenceScore is < 0 or > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(confidenceScore), "Confidence score must be between 0 and 1.");
        }

        Id = id ?? Guid.NewGuid();
        JobId = jobId;
        IsSuccessful = isSuccessful;
        Summary = summary.Trim();
        ProcessedOn = processedOn;
        DetectedTradeType = detectedTradeType;
        ConfidenceScore = confidenceScore;
        ErrorMessage = string.IsNullOrWhiteSpace(errorMessage) ? null : errorMessage.Trim();
    }

    public Guid Id { get; private set; }

    public Guid JobId { get; private set; }

    public bool IsSuccessful { get; private set; }

    public string Summary { get; private set; }

    public DateTime ProcessedOn { get; private set; }

    public TradeType DetectedTradeType { get; private set; }

    public decimal? ConfidenceScore { get; private set; }

    public string? ErrorMessage { get; private set; }
}
