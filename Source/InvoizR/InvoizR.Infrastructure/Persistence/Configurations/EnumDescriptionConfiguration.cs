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

        // Set configuration for columns
        builder
            .Property(p => p.Id)
            .HasColumnType("int")
            .IsRequired()
            ;

        builder
            .Property(p => p.Desc)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired()
            ;

        builder
            .Property(p => p.FullName)
            .HasColumnType("nvarchar")
            .HasMaxLength(200)
            .IsRequired()
            ;
    }
}
