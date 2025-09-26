using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class ThirdPartyServiceConfiguration : IEntityTypeConfiguration<ThirdPartyService>
{
    public void Configure(EntityTypeBuilder<ThirdPartyService> builder)
    {
        // Set configuration for entity
        builder.ToTable("ThirdPartyService", "dbo");

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
            .Property(p => p.EnvironmentId)
            .HasColumnType("nvarchar")
            .HasMaxLength(2)
            .IsRequired()
            ;

        builder
            .Property(p => p.Name)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired()
            ;

        builder
            .Property(p => p.Description)
            .HasColumnType("nvarchar")
            .HasMaxLength(200)
            .IsRequired()
            ;

        // Add configuration for uniques

        builder
            .HasIndex(p => new { p.EnvironmentId, p.Name })
            .IsUnique()
            .HasDatabaseName("UQ_dbo_ThirdPartyService_EnvironmentId_Name")
            ;
    }
}
