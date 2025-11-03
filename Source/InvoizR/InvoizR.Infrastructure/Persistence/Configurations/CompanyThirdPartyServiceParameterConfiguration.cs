using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class CompanyThirdPartyServiceParameterConfiguration : IEntityTypeConfiguration<CompanyThirdPartyServiceParameter>
{
    public void Configure(EntityTypeBuilder<CompanyThirdPartyServiceParameter> builder)
    {
        // Set configuration for entity
        builder.ToTable("CompanyThirdPartyServiceParameter", "dbo");

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
            .IsRequired()
            ;

        builder
            .Property(p => p.ThirdPartyServiceId)
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
            .Property(p => p.Category)
            .HasColumnType("nvarchar")
            .HasMaxLength(25)
            .IsRequired()
            ;

        builder
            .Property(p => p.Name)
            .HasColumnType("nvarchar")
            .HasMaxLength(25)
            .IsRequired()
            ;

        builder
            .Property(p => p.Value)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .IsRequired()
            ;

        // Add configuration for uniques

        builder
            .HasIndex(p => new { p.CompanyId, p.ThirdPartyServiceId, p.EnvironmentId, p.Category, p.Name })
            .IsUnique()
            .HasDatabaseName("UQ_dbo_CompanyThirdPartyServiceParameter_CompanyId_ThirdPartyServiceId_EnvironmentId_Category_Name")
            ;

        // Add configuration for foreign keys

        builder
            .HasOne(p => p.Company)
            .WithMany(b => b.CompanyThirdPartyServiceParameters)
            .HasForeignKey(p => p.CompanyId)
            .HasConstraintName("FK_dbo_CompanyThirdPartyServiceParameter_CompanyId_dbo_Company")
            ;

        builder
            .HasOne(p => p.ThirdPartyService)
            .WithMany(b => b.CompanyThirdPartyServiceParameters)
            .HasForeignKey(p => p.ThirdPartyServiceId)
            .HasConstraintName("FK_dbo_CompanyThirdPartyServiceParameter_ThirdPartyServiceId_dbo_ThirdPartyService")
            ;
    }
}
