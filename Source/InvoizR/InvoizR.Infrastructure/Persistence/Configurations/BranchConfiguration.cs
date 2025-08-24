using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        // Set configuration for entity
        builder.ToTable("Branch", "dbo");

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
            .Property(p => p.Name)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .IsRequired()
            ;

        builder
            .Property(p => p.EstablishmentPrefix)
            .HasColumnType("nvarchar")
            .HasMaxLength(5)
            .IsRequired()
            ;

        builder
            .Property(p => p.TaxAuthId)
            .HasColumnType("nvarchar")
            .HasMaxLength(4)
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
            .Property(p => p.ResponsibleId)
            .HasColumnType("smallint")
            ;

        // Add configuration for uniques

        builder
            .HasIndex(p => new { p.CompanyId, p.Name })
            .IsUnique()
            .HasDatabaseName("UQ_dbo_Branch_CompanyId_Name")
            ;

        builder
            .Property(p => p.NonCustomerEmail)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            ;

        // Add configuration for foreign keys

        builder
            .HasOne(p => p.Company)
            .WithMany(b => b.Branches)
            .HasForeignKey(p => p.CompanyId)
            .HasConstraintName("FK_dbo_Branch_CompanyId_dbo_Company")
            ;

        builder
            .HasOne(p => p.Responsible)
            .WithMany(b => b.Branches)
            .HasForeignKey(p => p.ResponsibleId)
            .HasConstraintName("FK_dbo_Branch_ResponsibleId_dbo_Responsible")
            ;
    }
}
