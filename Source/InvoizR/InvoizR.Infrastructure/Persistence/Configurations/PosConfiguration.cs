using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class PosConfiguration : IEntityTypeConfiguration<Pos>
{
    public void Configure(EntityTypeBuilder<Pos> builder)
    {
        // Set configuration for entity
        builder.ToTable("Pos", "dbo");

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
            .Property(p => p.Name)
            .HasColumnType("nvarchar")
            .HasMaxLength(25)
            .IsRequired()
            ;

        builder
            .Property(p => p.Code)
            .HasColumnType("nvarchar")
            .HasMaxLength(5)
            .IsRequired()
            ;

        builder
            .Property(p => p.TaxAuthPos)
            .HasColumnType("nvarchar")
            .HasMaxLength(4)
            ;

        builder
            .Property(p => p.Description)
            .HasColumnType("nvarchar(max)")
            ;

        // Add configuration for uniques

        builder
            .HasIndex(p => new { p.BranchId, p.Name })
            .IsUnique()
            .HasDatabaseName("UQ_dbo_Pos_BranchId_Name")
            ;

        // Add configuration for foreign keys

        builder
            .HasOne(p => p.Branch)
            .WithMany(b => b.Pos)
            .HasForeignKey(p => p.BranchId)
            .HasConstraintName("FK_dbo_Pos_BranchId_dbo_Branch")
            ;
    }
}
