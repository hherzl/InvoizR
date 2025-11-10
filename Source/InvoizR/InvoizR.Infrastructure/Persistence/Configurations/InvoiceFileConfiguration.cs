using InvoizR.Domain.Entities;
using InvoizR.Infrastructure.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class InvoiceFileConfiguration : AuditableEntityConfiguration<InvoiceFile>
{
    public override void Configure(EntityTypeBuilder<InvoiceFile> builder)
    {
        base.Configure(builder);

        // Set configuration for entity
        builder.ToTable("InvoiceFile", "dbo");

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
            .Property(p => p.Size)
            .HasColumnType("bigint")
            .IsRequired()
            ;

        builder
            .Property(p => p.MimeType)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .IsRequired()
            ;

        builder
            .Property(p => p.FileType)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .IsRequired()
            ;

        builder
            .Property(p => p.FileName)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .IsRequired()
            ;

        builder
            .Property(p => p.ExternalUrl)
            .HasColumnType("nvarchar")
            .HasMaxLength(200)
            ;

        builder
            .Property(p => p.File)
            .HasColumnType("varbinary(max)")
            .IsRequired()
            ;

        // Add configuration for uniques

        builder
            .HasIndex(p => new { p.InvoiceId, p.FileName })
            .IsUnique()
            .HasDatabaseName("UQ_dbo_InvoiceFile_InvoiceId_FileName")
            ;
    }
}
