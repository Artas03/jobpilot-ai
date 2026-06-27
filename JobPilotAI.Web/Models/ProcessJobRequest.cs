using System.ComponentModel.DataAnnotations;

namespace JobPilotAI.Web.Models;

public sealed class ProcessJobRequest
{
    [Required(ErrorMessage = "Select a trade.")]
    public string TradeType { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter the customer's name.")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter the job address.")]
    public string JobAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "Add some job notes.")]
    public string RawNotes { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "1000", ErrorMessage = "Labour hours cannot be negative.")]
    public decimal LabourHours { get; set; }

    [Range(typeof(decimal), "0", "100000", ErrorMessage = "Hourly rate cannot be negative.")]
    public decimal HourlyRate { get; set; }

    [Range(typeof(decimal), "0", "1000000", ErrorMessage = "Materials cost cannot be negative.")]
    public decimal MaterialsCost { get; set; }
}
