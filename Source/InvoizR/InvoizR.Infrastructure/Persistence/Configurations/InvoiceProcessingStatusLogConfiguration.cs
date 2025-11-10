using InvoizR.Domain.Entities;
using InvoizR.Infrastructure.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class InvoiceProcessingStatusLogConfiguration : AuditableEntityConfiguration<InvoiceProcessingStatusLog>
{
    public override void Configure(EntityTypeBuilder<InvoiceProcessingStatusLog> builder)
    {
        base.Configure(builder);

        // Set configuration for entity
        builder.ToTable("InvoiceProcessingStatusLog", "dbo");

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
    }
}
