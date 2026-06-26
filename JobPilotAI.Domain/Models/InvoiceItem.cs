namespace JobPilotAI.Domain.Models;

public class InvoiceItem
{
    public InvoiceItem(
        string description,
        decimal quantity,
        decimal unitPrice,
        Guid? id = null)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Invoice item description is required.", nameof(description));
        }

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        }

        if (unitPrice < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative.");
        }

        Id = id ?? Guid.NewGuid();
        Description = description.Trim();
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public Guid Id { get; private set; }

    public string Description { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal Total => Quantity * UnitPrice;
}
