using InvoizR.Domain.Entities;
using InvoizR.Infrastructure.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class InvoiceSyncLogConfiguration : AuditableEntityConfiguration<InvoiceSyncLog>
{
    public override void Configure(EntityTypeBuilder<InvoiceSyncLog> builder)
    {
        base.Configure(builder);

        // Set configuration for entity
        builder.ToTable("InvoiceSyncLog", "dbo");

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
            .Property(p => p.SyncStatusId)
            .HasColumnType("smallint")
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
            .HasMaxLength(50)
            .IsRequired()
            ;

        builder
            .Property(p => p.Content)
            .HasColumnType("nvarchar(max)")
            .IsRequired()
            ;
    }
}
