using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class InvoiceCancellationLogConfiguration : IEntityTypeConfiguration<InvoiceCancellationLog>
{
    public void Configure(EntityTypeBuilder<InvoiceCancellationLog> builder)
    {
        // Set configuration for entity
        builder.ToTable("InvoiceCancellationLog", "dbo");

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
            .Property(p => p.InvoiceId)
            .HasColumnType("bigint")
            .IsRequired()
            ;

        builder
            .Property(p => p.ProcessingStatusId)
            .HasColumnType("smallint")
            .IsRequired()
            ;

        builder
            .Property(p => p.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired()
            ;

        builder
            .Property(p => p.LogType)
            .HasColumnType("nvarchar")
            .HasMaxLength(25)
            .IsRequired()
            ;

        builder
            .Property(p => p.ContentType)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .IsRequired()
            ;

        builder
            .Property(p => p.Payload)
            .HasColumnType("nvarchar(max)")
            .IsRequired()
            ;

        // Add configuration for foreign keys

        builder
            .HasOne(p => p.Invoice)
            .WithMany(b => b.InvoiceCancellationLogs)
            .HasForeignKey(p => p.InvoiceId)
            .HasConstraintName("FK_dbo_InvoiceCancellationLog_InvoiceId_dbo_Invoice")
            ;
    }
}
