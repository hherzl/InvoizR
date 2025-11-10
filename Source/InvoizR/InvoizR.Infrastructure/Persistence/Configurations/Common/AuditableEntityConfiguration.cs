using InvoizR.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations.Common;

internal class AuditableEntityConfiguration<TAuditableEntity> : IEntityTypeConfiguration<TAuditableEntity> where TAuditableEntity : AuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<TAuditableEntity> builder)
    {
        builder
            .Property(p => p.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired()
            ;

        builder
            .Property(p => p.CreatedBy)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired()
            ;

        builder
            .Property(p => p.LastModifiedAt)
            .HasColumnType("datetime")
            ;

        builder
            .Property(p => p.LastModifiedBy)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            ;

        builder
            .Property(p => p.RowVersion)
            .HasColumnType("rowversion")
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken()
            ;
    }
}
