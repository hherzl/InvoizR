using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class InvoiceTypeConfiguration : IEntityTypeConfiguration<InvoiceType>
{
    public void Configure(EntityTypeBuilder<InvoiceType> builder)
    {
        // Set configuration for entity
        builder.ToTable("InvoiceType", "dbo");

        // Set key for entity
        builder.HasKey(p => p.Id);

        // Set configuration for columns
        builder
            .Property(p => p.Id)
            .HasColumnType("smallint")
            .IsRequired()
            ;

        builder
            .Property(p => p.Name)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .IsRequired()
            ;

        builder
            .Property(p => p.SchemaType)
            .HasColumnType("nvarchar")
            .HasMaxLength(2)
            .IsRequired()
            ;

        builder
            .Property(p => p.SchemaVersion)
            .HasColumnType("smallint")
            .IsRequired()
            ;

        builder
            .Property(p => p.Current)
            .HasColumnType("bit")
            .IsRequired()
            ;

        builder
            .Property(p => p.CancellationPeriodInDays)
            .HasColumnType("smallint")
            .IsRequired()
            ;

        // Add configuration for uniques

        builder
            .HasIndex(p => p.Name)
            .IsUnique()
            .HasDatabaseName("UQ_dbo_InvoiceType_Name")
            ;
    }
}
