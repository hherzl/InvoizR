using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class EnumDescriptionConfiguration : IEntityTypeConfiguration<EnumDescription>
{
    public void Configure(EntityTypeBuilder<EnumDescription> builder)
    {
        // Set configuration for entity
        builder.ToTable("EnumDescription", "dbo");

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
            .Property(p => p.FullName)
            .HasColumnType("nvarchar")
            .HasMaxLength(200)
            .IsRequired()
            ;

        builder
            .Property(p => p.Value)
            .HasColumnType("int")
            .IsRequired()
            ;

        builder
            .Property(p => p.Desc)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired()
            ;

        // Add configuration for uniques

        builder
            .HasIndex(p => new { p.FullName, p.Value })
            .IsUnique()
            .HasDatabaseName("UQ_dbo_EnumDescription_FullName_Value")
            ;
    }
}
