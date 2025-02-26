using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class VInvoiceProcessingStatusConfiguration : IEntityTypeConfiguration<VInvoiceProcessingStatus>
{
    public void Configure(EntityTypeBuilder<VInvoiceProcessingStatus> builder)
    {
        // Set configuration for entity
        builder.ToView("VInvoiceProcessingStatus", "dbo");

        // Set key for entity
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnType("int").IsRequired();
        builder.Property(p => p.Desc).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
    }
}
