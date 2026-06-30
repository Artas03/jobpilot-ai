using Microsoft.EntityFrameworkCore;

namespace JobPilotAI.Infrastructure.Persistence;

public sealed class JobPilotDbContext(DbContextOptions<JobPilotDbContext> options)
    : DbContext(options)
{
    public DbSet<ProcessedJobEntity> ProcessedJobs => Set<ProcessedJobEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var job = modelBuilder.Entity<ProcessedJobEntity>();

        job.ToTable("ProcessedJobs");
        job.HasKey(entity => entity.Id);
        job.Property(entity => entity.TradeType).HasConversion<string>().HasMaxLength(50);
        job.Property(entity => entity.CustomerName).HasMaxLength(200).IsRequired();
        job.Property(entity => entity.CustomerEmail).HasMaxLength(320);
        job.Property(entity => entity.CustomerPhone).HasMaxLength(50);
        job.Property(entity => entity.JobAddress).HasMaxLength(500);
        job.Property(entity => entity.RawNotes).IsRequired();
        job.Property(entity => entity.LabourHours).HasPrecision(18, 2);
        job.Property(entity => entity.HourlyRate).HasPrecision(18, 2);
        job.Property(entity => entity.MaterialsCost).HasPrecision(18, 2);
        job.Property(entity => entity.InvoiceSubtotal).HasPrecision(18, 2);
        job.Property(entity => entity.InvoiceNumber).HasMaxLength(100);
        job.Property(entity => entity.InvoiceTaxAmount).HasPrecision(18, 2);
        job.Property(entity => entity.InvoiceTotal).HasPrecision(18, 2);
        job.Property(entity => entity.SuggestedNextActionsJson).IsRequired();
        job.Property(entity => entity.InvoiceItemsJson).IsRequired();
        job.HasIndex(entity => entity.CreatedAt);
    }
}
