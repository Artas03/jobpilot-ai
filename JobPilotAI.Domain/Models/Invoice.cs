namespace JobPilotAI.Domain.Models;

public class Invoice
{
    private readonly List<InvoiceItem> _items = [];

    public Invoice(
        Guid jobId,
        string invoiceNumber,
        DateTime issuedOn,
        DateTime? dueOn = null,
        IEnumerable<InvoiceItem>? items = null,
        decimal taxRate = 0,
        Guid? id = null)
    {
        if (jobId == Guid.Empty)
        {
            throw new ArgumentException("Job id is required.", nameof(jobId));
        }

        if (string.IsNullOrWhiteSpace(invoiceNumber))
        {
            throw new ArgumentException("Invoice number is required.", nameof(invoiceNumber));
        }

        if (taxRate < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(taxRate), "Tax rate cannot be negative.");
        }

        Id = id ?? Guid.NewGuid();
        JobId = jobId;
        InvoiceNumber = invoiceNumber.Trim();
        IssuedOn = issuedOn;
        DueOn = dueOn;
        TaxRate = taxRate;

        if (items is not null)
        {
            _items.AddRange(items);
        }
    }

    public Guid Id { get; private set; }

    public Guid JobId { get; private set; }

    public string InvoiceNumber { get; private set; }

    public DateTime IssuedOn { get; private set; }

    public DateTime? DueOn { get; private set; }

    public decimal TaxRate { get; private set; }

    public DateTime? PaidOn { get; private set; }

    public IReadOnlyCollection<InvoiceItem> Items => _items.AsReadOnly();

    public decimal Subtotal => _items.Sum(item => item.Total);

    public decimal TaxAmount => Subtotal * TaxRate;

    public decimal Total => Subtotal + TaxAmount;

    public bool IsPaid => PaidOn.HasValue;

    public void AddItem(InvoiceItem item)
    {
        ArgumentNullException.ThrowIfNull(item);
        _items.Add(item);
    }

    public void MarkPaid(DateTime paidOn)
    {
        PaidOn = paidOn;
    }
}
