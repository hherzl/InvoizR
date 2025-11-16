using InvoizR.Domain.Entities;
using InvoizR.Infrastructure.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class InvoiceWebhookNotificationConfiguration : AuditableEntityConfiguration<InvoiceWebhookNotification>
{
    public override void Configure(EntityTypeBuilder<InvoiceWebhookNotification> builder)
    {
        base.Configure(builder);

        // Set configuration for entity
        builder.ToTable("InvoiceWebhookNotification", "dbo");

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
            .Property(p => p.Protocol)
            .HasColumnType("nvarchar")
            .HasMaxLength(25)
            .IsRequired()
            ;

        builder
            .Property(p => p.Address)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .IsRequired()
            ;

        builder
            .Property(p => p.ContentType)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired()
            ;

        builder
            .Property(p => p.IsSuccess)
            .HasColumnType("bit")
            .IsRequired()
            ;

        builder
            .Property(p => p.Request)
            .HasColumnType("nvarchar(max)")
            .IsRequired()
            ;

        builder
            .Property(p => p.Response)
            .HasColumnType("nvarchar(max)")
            .IsRequired()
            ;

        // Add configuration for foreign keys

        builder
            .HasOne(p => p.Invoice)
            .WithMany(b => b.InvoiceWebhookNotifications)
            .HasForeignKey(p => p.InvoiceId)
            .HasConstraintName("FK_dbo_InvoiceWebhookNotification_InvoiceId_dbo_Invoice")
            ;
    }
}
