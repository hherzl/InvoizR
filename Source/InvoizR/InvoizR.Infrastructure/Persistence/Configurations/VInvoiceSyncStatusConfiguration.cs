using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class VInvoiceSyncStatusConfiguration : IEntityTypeConfiguration<VInvoiceSyncStatus>
{
    public void Configure(EntityTypeBuilder<VInvoiceSyncStatus> builder)
    {
        // Set configuration for entity
        builder.ToView("VInvoiceSyncStatus", "dbo");

        builder.HasNoKey();

        builder.Property(p => p.Id).HasColumnType("int").IsRequired();
        builder.Property(p => p.Desc).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
    }
}
