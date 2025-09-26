﻿using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoizR.Infrastructure.Persistence.Configurations;

internal class ThirdPartyServiceParameterConfiguration : IEntityTypeConfiguration<ThirdPartyServiceParameter>
{
    public void Configure(EntityTypeBuilder<ThirdPartyServiceParameter> builder)
    {
        // Set configuration for entity
        builder.ToTable("ThirdPartyServiceParameter", "dbo");

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
            .Property(p => p.ThirdPartyServiceId)
            .HasColumnType("smallint")
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

        builder
            .Property(p => p.RequiresEncryption)
            .HasColumnType("bit")
            ;

        // Add configuration for uniques

        builder
            .HasIndex(p => new { p.ThirdPartyServiceId, p.Category, p.Name })
            .IsUnique()
            .HasDatabaseName("UQ_dbo_ThirdPartyServiceParameter_ThirdPartyServiceId_Category_Name")
            ;

        // Add configuration for foreign keys

        builder
            .HasOne(p => p.ThirdPartyService)
            .WithMany(b => b.ThirdPartyServiceParameters)
            .HasForeignKey(p => p.ThirdPartyServiceId)
            .HasConstraintName("FK_dbo_ThirdPartyServiceParameter_ThirdPartyServiceId_dbo_ThirdPartyService")
            ;
    }
}
