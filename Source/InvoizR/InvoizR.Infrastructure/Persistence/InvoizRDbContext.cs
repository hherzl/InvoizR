using System.Reflection;
using InvoizR.Application.Common.Contracts;
using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Infrastructure.Persistence;

public partial class InvoizRDbContext : DbContext, IInvoizRDbContext
{
    private readonly IMediator _mediator;

    public InvoizRDbContext(DbContextOptions<InvoizRDbContext> options, IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<ThirdPartyService> ThirdPartyServices { get; set; }
    public DbSet<ThirdPartyServiceParameter> ThirdPartyServiceParameters { get; set; }
    public DbSet<EnumDescription> EnumDescriptions { get; set; }

    public DbSet<Fallback> Fallbacks { get; set; }
    public DbSet<FallbackProcessingLog> FallbackProcessingLogs { get; set; }
    public DbSet<FallbackFile> FallbackFiles { get; set; }

    public DbSet<Responsible> Responsibles { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<BranchNotification> BranchNotifications { get; set; }
    public DbSet<Pos> PointOfSales { get; set; }

    public DbSet<InvoiceType> InvoiceTypes { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceSyncStatusLog> InvoiceSyncStatusLogs { get; set; }
    public DbSet<InvoiceSyncLog> InvoiceSyncLogs { get; set; }
    public DbSet<InvoiceNotification> InvoiceNotifications { get; set; }
    public DbSet<InvoiceFile> InvoiceFiles { get; set; }
    public DbSet<InvoiceWebhookNotification> InvoiceWebhookNotifications { get; set; }
    public DbSet<InvoiceCancellationLog> InvoiceCancellationLogs { get; set; }

    public DbSet<VInvoiceSyncStatus> VInvoiceSyncStatuses { get; set; }
    public DbSet<VInvoiceProcessingType> VInvoiceProcessingTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
            ;

        base.OnModelCreating(modelBuilder);
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added && entry.Entity is AuditableEntity addedEntity)
            {
                addedEntity.CreatedAt = DateTime.Now;
                addedEntity.CreatedBy = "user";
            }
            else if (entry.State == EntityState.Modified && entry.Entity is AuditableEntity modifiedEntity)
            {
                modifiedEntity.LastModifiedAt = DateTime.Now;
                modifiedEntity.LastModifiedBy = "user";
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task DispatchNotificationsAsync(CancellationToken cancellationToken = default)
    {
        await _mediator?.DispatchNotificationsAsync(this);
    }
}
