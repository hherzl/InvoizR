using System.Reflection;
using InvoizR.Application.Common.Contracts;
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

    public DbSet<ThirdPartyService> ThirdPartyService { get; set; }
    public DbSet<ThirdPartyServiceParameter> ThirdPartyServiceParameter { get; set; }
    public DbSet<EnumDescription> EnumDescription { get; set; }
    public DbSet<Fallback> Fallback { get; set; }
    public DbSet<FallbackProcessingLog> FallbackProcessingLog { get; set; }
    public DbSet<FallbackFile> FallbackFile { get; set; }
    public DbSet<Responsible> Responsible { get; set; }
    public DbSet<Company> Company { get; set; }
    public DbSet<Branch> Branch { get; set; }
    public DbSet<BranchNotification> BranchNotification { get; set; }
    public DbSet<Pos> Pos { get; set; }
    public DbSet<InvoiceType> InvoiceType { get; set; }
    public DbSet<Invoice> Invoice { get; set; }
    public DbSet<InvoiceProcessingStatusLog> InvoiceProcessingStatusLog { get; set; }
    public DbSet<InvoiceProcessingLog> InvoiceProcessingLog { get; set; }
    public DbSet<InvoiceNotification> InvoiceNotification { get; set; }
    public DbSet<InvoiceFile> InvoiceFile { get; set; }
    public DbSet<InvoiceCancellationLog> InvoiceCancellationLog { get; set; }

    public DbSet<VInvoiceProcessingStatus> VInvoiceProcessingStatus { get; set; }
    public DbSet<VInvoiceProcessingType> VInvoiceProcessingType { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
            ;

        base.OnModelCreating(modelBuilder);
    }

    public async Task DispatchNotificationsAsync(CancellationToken cancellationToken = default)
    {
        await _mediator?.DispatchNotificationsAsync(this);
    }
}
