using System.Reflection;
using InvoizR.Application.Common.Persistence;
using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Infrastructure.Persistence;

public partial class InvoizRDbContext : DbContext, IInvoizRDbContext
{
    public InvoizRDbContext(DbContextOptions<InvoizRDbContext> options)
        : base(options)
    {
    }

    public DbSet<EnumDescription> EnumDescription { get; set; }
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

    public DbSet<VInvoiceProcessingStatus> VInvoiceProcessingStatus { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
            ;

        base.OnModelCreating(modelBuilder);
    }
}
