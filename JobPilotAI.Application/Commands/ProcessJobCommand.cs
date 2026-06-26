using JobPilotAI.Domain.Enums;

namespace JobPilotAI.Application.Commands;

public class ProcessJobCommand
{
    public ProcessJobCommand(
        TradeType tradeType,
        string customerName,
        string jobAddress,
        string rawNotes,
        decimal labourHours,
        decimal hourlyRate,
        decimal materialsCost)
    {
        TradeType = tradeType;
        CustomerName = customerName;
        JobAddress = jobAddress;
        RawNotes = rawNotes;
        LabourHours = labourHours;
        HourlyRate = hourlyRate;
        MaterialsCost = materialsCost;
    }

    public TradeType TradeType { get; }

    public string CustomerName { get; }

    public string JobAddress { get; }

    public string RawNotes { get; }

    public decimal LabourHours { get; }

    public decimal HourlyRate { get; }

    public decimal MaterialsCost { get; }
}
