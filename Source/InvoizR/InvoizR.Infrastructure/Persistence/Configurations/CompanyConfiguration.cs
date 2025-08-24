using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
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
            .Property(p => p.TaxRegistrationNumber)
            .HasColumnType("nvarchar")
            .HasMaxLength(20)
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
    }
}
