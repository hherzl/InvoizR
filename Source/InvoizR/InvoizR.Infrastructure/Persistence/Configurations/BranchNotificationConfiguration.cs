using InvoizR.Domain.Entities;
using InvoizR.Infrastructure.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class BranchNotificationConfiguration : AuditableEntityConfiguration<BranchNotification>
{
    public override void Configure(EntityTypeBuilder<BranchNotification> builder)
    {
        base.Configure(builder);

        // Set configuration for entity
        builder.ToTable("BranchNotification", "dbo");

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
            .Property(p => p.BranchId)
            .HasColumnType("smallint")
            .IsRequired()
            ;

        builder
            .Property(p => p.InvoiceTypeId)
            .HasColumnType("smallint")
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

        // Add configuration for uniques

        builder
            .HasIndex(p => new { p.BranchId, p.InvoiceTypeId, p.Email })
            .IsUnique()
            .HasDatabaseName("UQ_dbo_BranchNotification_BranchId_InvoiceTypeId_Email")
            ;

        // Add configuration for foreign keys

        builder
            .HasOne(p => p.Branch)
            .WithMany(b => b.BranchNotifications)
            .HasForeignKey(p => p.BranchId)
            .HasConstraintName("FK_dbo_BranchNotification_BranchId_dbo_Branch")
            ;

        builder
            .HasOne(p => p.InvoiceType)
            .WithMany(b => b.BranchNotifications)
            .HasForeignKey(p => p.InvoiceTypeId)
            .HasConstraintName("FK_dbo_BranchNotification_InvoiceTypeId_dbo_InvoiceType")
            ;
    }
}
