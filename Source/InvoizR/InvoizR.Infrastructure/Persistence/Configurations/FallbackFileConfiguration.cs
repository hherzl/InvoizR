using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class FallbackFileConfiguration : IEntityTypeConfiguration<FallbackFile>
{
    public void Configure(EntityTypeBuilder<FallbackFile> builder)
    {
        // Set configuration for entity
        builder.ToTable("FallbackFile", "dbo");

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
            .Property(p => p.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired()
            ;

        builder
            .Property(p => p.File)
            .HasColumnType("varbinary(max)")
            .IsRequired();

        // Add configuration for uniques

        builder
            .HasIndex(p => new { p.FallbackId, p.FileName })
            .IsUnique()
            .HasDatabaseName("UQ_dbo_FallbackFile_FallbackId_FileName")
            ;

        // Add configuration for foreign keys

        builder
            .HasOne(p => p.FallbackFk)
            .WithMany(b => b.FallbackFiles)
            .HasForeignKey(p => p.FallbackId)
            .HasConstraintName("FK_dbo_FallbackFile_FallbackId_dbo_Fallback")
            ;
    }
}
