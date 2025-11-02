using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        // Set configuration for entity
        builder.ToTable("Invoice", "dbo");

        // Set key for entity
        builder.HasKey(p => p.Id);

        // Set identity for entity (auto increment)
        builder.Property(p => p.Id).UseIdentityColumn();

        // Set configuration for columns
        builder
            .Property(p => p.Id)
            .HasColumnType("bigint")
            .IsRequired()
            ;

        builder
            .Property(p => p.FallbackId)
            .HasColumnType("smallint")
            ;

        builder
            .Property(p => p.PosId)
            .HasColumnType("smallint")
            .IsRequired()
            ;

        builder
            .Property(p => p.CustomerId)
            .HasColumnType("nvarchar")
            .HasMaxLength(30)
            ;

        builder
            .Property(p => p.CustomerDocumentTypeId)
            .HasColumnType("nvarchar")
            .HasMaxLength(2)
            ;

        builder
            .Property(p => p.CustomerDocumentNumber)
            .HasColumnType("nvarchar")
            .HasMaxLength(25)
            ;

        builder
            .Property(p => p.CustomerWtId)
            .HasColumnType("nvarchar")
            .HasMaxLength(5)
            ;

        builder
            .Property(p => p.CustomerName)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            ;

        builder
            .Property(p => p.CustomerCountryId)
            .HasColumnType("nvarchar")
            .HasMaxLength(3)
            ;

        builder
            .Property(p => p.CustomerCountryLevelId)
            .HasColumnType("smallint")
            ;

        builder
            .Property(p => p.CustomerAddress)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            ;

        builder
            .Property(p => p.CustomerPhone)
            .HasColumnType("nvarchar")
            .HasMaxLength(25)
            ;

        builder
            .Property(p => p.CustomerEmail)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            ;

        builder
            .Property(p => p.CustomerLastUpdated)
            .HasColumnType("datetime")
            ;

        builder
            .Property(p => p.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired()
            ;

        builder
            .Property(p => p.InvoiceTypeId)
            .HasColumnType("smallint")
            .IsRequired()
            ;

        builder
            .Property(p => p.InvoiceNumber)
            .HasColumnType("bigint")
            .IsRequired()
            ;

        builder
            .Property(p => p.InvoiceDate)
            .HasColumnType("datetime")
            .IsRequired()
            ;

        builder
            .Property(p => p.InvoiceTotal)
            .HasColumnType("decimal(14, 6)")
            .IsRequired()
            ;

        builder
            .Property(p => p.Lines)
            .HasColumnType("int")
            .IsRequired()
            ;

        builder
            .Property(p => p.SchemaType)
            .HasColumnType("nvarchar")
            .HasMaxLength(2)
            ;

        builder
            .Property(p => p.SchemaVersion)
            .HasColumnType("smallint")
            ;

        builder
            .Property(p => p.GenerationCode)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            ;

        builder
            .Property(p => p.ControlNumber)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            ;

        builder
            .Property(p => p.Payload)
            .HasColumnType("nvarchar(max)")
            ;

        builder
            .Property(p => p.ProcessingTypeId)
            .HasColumnType("smallint")
            .IsRequired()
            ;

        builder
            .Property(p => p.ProcessingStatusId)
            .HasColumnType("smallint")
            .IsRequired()
            ;

        builder
            .Property(p => p.RetryIn)
            .HasColumnType("int")
            ;

        builder
            .Property(p => p.SyncAttempts)
            .HasColumnType("int")
            ;

        builder
            .Property(p => p.EmitDateTime)
            .HasColumnType("datetime")
            ;

        builder
            .Property(p => p.ReceiptStamp)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            ;

        builder
            .Property(p => p.CancellationPayload)
            .HasColumnType("nvarchar")
            ;

        builder
            .Property(p => p.CancellationProcessingStatusId)
            .HasColumnType("smallint")
            ;

        builder
            .Property(p => p.CancellationDateTime)
            .HasColumnType("datetime")
            ;

        builder
            .Property(p => p.ExternalUrl)
            .HasColumnType("nvarchar")
            .HasMaxLength(125)
            ;

        builder
            .Property(p => p.Notes)
            .HasColumnType("nvarchar(max)")
            ;

        // Add configuration for uniques

        builder
            .HasIndex(p => new { p.InvoiceTypeId, p.InvoiceNumber })
            .IsUnique()
            .HasDatabaseName("UQ_dbo_Invoice_InvoiceTypeId_InvoiceNumber")
            ;

        // Add configuration for foreign keys

        builder
            .HasOne(p => p.Pos)
            .WithMany(b => b.Invoices)
            .HasForeignKey(p => p.PosId)
            .HasConstraintName("FK_dbo_Invoice_PosId_dbo_Pos")
            ;
    }
}
