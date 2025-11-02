using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class FallbackConfiguration : IEntityTypeConfiguration<Fallback>
{
    public void Configure(EntityTypeBuilder<Fallback> builder)
    {
        // Set configuration for entity
        builder.ToTable("Fallback", "dbo");

        // Set key for entity
        builder.HasKey(p => p.Id);

        // Set identity for entity (auto increment)
        builder.Property(p => p.Id).UseIdentityColumn();

        // Set configuration for columns
        builder
            .Property(p => p.Id)
            .HasColumnType("smallint")
            .IsRequired()
            ;

        builder
            .Property(p => p.CompanyId)
            .HasColumnType("smallint")
            ;

        builder
            .Property(p => p.Name)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .IsRequired()
            ;

        builder
            .Property(p => p.StartDateTime)
            .HasColumnType("datetime")
            .IsRequired()
            ;

        builder
            .Property(p => p.EndDateTime)
            .HasColumnType("datetime")
            ;

        builder
            .Property(p => p.Enable)
            .HasColumnType("bit")
            .IsRequired()
            ;

        builder
            .Property(p => p.FallbackGuid)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            ;

        builder
            .Property(p => p.SyncStatusId)
            .HasColumnType("smallint")
            .IsRequired()
            ;

        builder
            .Property(p => p.Payload)
            .HasColumnType("nvarchar(max)")
            .IsRequired()
            ;

        builder
            .Property(p => p.RetryIn)
            .HasColumnType("int")
            .IsRequired()
            ;

        builder
            .Property(p => p.SyncAttempts)
            .HasColumnType("int")
            .IsRequired()
            ;

        builder
            .Property(p => p.EmitDateTime)
            .HasColumnType("datetime")
            ;

        builder
            .Property(p => p.ReceiptStamp)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            ;

        // Add configuration for uniques

        builder
            .HasIndex(p => new { p.CompanyId, p.Name })
            .IsUnique()
            .HasDatabaseName("UQ_dbo_Fallback_CompanyId_Name")
            ;

        // Add configuration for foreign keys

        builder
            .HasOne(p => p.Company)
            .WithMany(b => b.Fallbacks)
            .HasForeignKey(p => p.CompanyId)
            .HasConstraintName("FK_dbo_Fallback_CompanyId_dbo_Company")
            ;
    }
}
