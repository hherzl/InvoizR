using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class ResponsibleConfiguration : IEntityTypeConfiguration<Responsible>
{
    public void Configure(EntityTypeBuilder<Responsible> builder)
    {
        // Set configuration for entity
        builder.ToTable("Responsible", "dbo");

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
            .Property(p => p.Name)
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .IsRequired()
            ;

        builder
            .Property(p => p.Phone)
            .HasColumnType("nvarchar(max)")
            .IsRequired()
            ;

        builder
            .Property(p => p.Email)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired()
            ;

        builder
            .Property(p => p.IdType)
            .HasColumnType("nvarchar")
            .HasMaxLength(2)
            .IsRequired()
            ;

        builder
            .Property(p => p.IdNumber)
            .HasColumnType("nvarchar")
            .HasMaxLength(2)
            .IsRequired()
            ;

        builder
            .Property(p => p.AuthorizeCancellation)
            .HasColumnType("bit")
            .IsRequired()
            ;

        builder
            .Property(p => p.AuthorizeContingency)
            .HasColumnType("bit")
            .IsRequired()
            ;

        // Add configuration for uniques

        builder
            .HasIndex(p => p.Email)
            .IsUnique()
            .HasDatabaseName("UQ_dbo_Responsible_Email")
            ;
    }
}
