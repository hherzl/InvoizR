using InvoizR.Domain.Entities;
using InvoizR.Infrastructure.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class InvoiceNotificationConfiguration : AuditableEntityConfiguration<InvoiceNotification>
{
    public override void Configure(EntityTypeBuilder<InvoiceNotification> builder)
    {
        base.Configure(builder);

        // Set configuration for entity
        builder.ToTable("InvoiceNotification", "dbo");

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
            .Property(p => p.Email)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired()
            ;

        builder
            .Property(p => p.Bcc)
            .HasColumnType("bit")
            .IsRequired()
            ;

        builder
            .Property(p => p.Files)
            .HasColumnType("smallint")
            .IsRequired()
            ;

        builder
            .Property(p => p.Successful)
            .HasColumnType("bit")
            .IsRequired()
            ;
    }
}
