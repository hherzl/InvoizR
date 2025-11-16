using InvoizR.Domain.Entities;
using InvoizR.Infrastructure.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class CompanyConfiguration : AuditableEntityConfiguration<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);

        // Set configuration for entity
        builder.ToTable("Company", "dbo");

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
            .Property(p => p.Environment)
            .HasColumnType("nvarchar")
            .HasMaxLength(2)
            .IsRequired()
            ;

        builder
            .Property(p => p.Name)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .IsRequired()
            ;

        builder
            .Property(p => p.Code)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired()
            ;

        builder
            .Property(p => p.BusinessName)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .IsRequired()
            ;

        builder
            .Property(p => p.TaxIdNumber)
            .HasColumnType("nvarchar")
            .HasMaxLength(25)
            .IsRequired()
            ;

        builder
            .Property(p => p.TaxpayerRegistrationNumber)
            .HasColumnType("nvarchar")
            .HasMaxLength(10)
            .IsRequired()
            ;

        builder
            .Property(p => p.EconomicActivityId)
            .HasColumnType("nvarchar")
            .HasMaxLength(10)
            .IsRequired()
            ;

        builder
            .Property(p => p.EconomicActivity)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired()
            ;

        builder
            .Property(p => p.CountryLevelId)
            .HasColumnType("smallint")
            .IsRequired()
            ;

        builder
            .Property(p => p.Address)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .IsRequired()
            ;

        builder
            .Property(p => p.Phone)
            .HasColumnType("nvarchar")
            .HasMaxLength(25)
            .IsRequired()
            ;

        builder
            .Property(p => p.Email)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired()
            ;

        builder
            .Property(p => p.Logo)
            .HasColumnType("varbinary(max)")
            ;

        builder
            .Property(p => p.Headquarters)
            .HasColumnType("int")
            ;

        builder
            .Property(p => p.NonCustomerEmail)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            ;

        builder
            .Property(p => p.WebhookNotificationProtocol)
            .HasColumnType("nvarchar")
            .HasMaxLength(25)
            ;

        builder
            .Property(p => p.WebhookNotificationAddress)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            ;

        builder
            .Property(p => p.WebhookNotificationMisc1)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            ;

        builder
            .Property(p => p.WebhookNotificationMisc2)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            ;

        // Add configuration for uniques

        builder
            .HasIndex(p => p.Name)
            .IsUnique()
            .HasDatabaseName("UQ_dbo_Company_Name")
            ;

        builder
            .HasIndex(p => p.BusinessName)
            .IsUnique()
            .HasDatabaseName("UQ_dbo_Company_BusinessName")
            ;

        builder
            .HasIndex(p => p.TaxIdNumber)
            .IsUnique()
            .HasDatabaseName("UQ_dbo_Company_TaxIdNumber")
            ;

        builder
            .HasIndex(p => p.TaxpayerRegistrationNumber)
            .IsUnique()
            .HasDatabaseName("UQ_dbo_Company_TaxpayerRegistrationNumber")
            ;
    }
}
