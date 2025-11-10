using InvoizR.Domain.Entities;
using InvoizR.Infrastructure.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class FallbackProcessingLogConfiguration : AuditableEntityConfiguration<FallbackProcessingLog>
{
    public override void Configure(EntityTypeBuilder<FallbackProcessingLog> builder)
    {
        base.Configure(builder);

        // Set configuration for entity
        builder.ToTable("FallbackProcessingLog", "dbo");

        // Set key for entity
        builder.HasKey(p => p.Id);

        // Set identity for entity (auto increment)
        builder.Property(p => p.Id).UseIdentityColumn();

        // Set configuration for columns
        builder
            .Property(p => p.Id)
            .HasColumnType("int")
            .IsRequired()
            ;

        builder
            .Property(p => p.FallbackId)
            .HasColumnType("smallint")
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
            .HasMaxLength(100)
            .IsRequired()
            ;

        builder
            .Property(p => p.Content)
            .HasColumnType("nvarchar(max)")
            .IsRequired()
            ;

        // Add configuration for foreign keys

        builder
            .HasOne(p => p.Fallback)
            .WithMany(b => b.FallbackProcessingLogs)
            .HasForeignKey(p => p.FallbackId)
            .HasConstraintName("FK_dbo_FallbackProcessingLog_FallbackId_dbo_Fallback")
            ;
    }
}
