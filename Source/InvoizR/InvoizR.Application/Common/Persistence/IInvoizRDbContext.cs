using InvoizR.Clients.DataContracts;
using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace InvoizR.Application.Common.Persistence;

public interface IInvoizRDbContext
{
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    DbSet<EnumDescription> EnumDescription { get; set; }
    DbSet<Responsible> Responsible { get; set; }
    DbSet<Company> Company { get; set; }
    DbSet<Branch> Branch { get; set; }
    DbSet<BranchNotification> BranchNotification { get; set; }
    DbSet<Pos> Pos { get; set; }
    DbSet<InvoiceType> InvoiceType { get; set; }
    DbSet<Invoice> Invoice { get; set; }
    DbSet<InvoiceProcessingStatusLog> InvoiceProcessingStatusLog { get; set; }
    DbSet<InvoiceProcessingLog> InvoiceProcessingLog { get; set; }
    DbSet<InvoiceNotification> InvoiceNotification { get; set; }
    DbSet<InvoiceFile> InvoiceFile { get; set; }

    DbSet<VInvoiceProcessingStatus> VInvoiceProcessingStatus { get; set; }

    Task<InvoiceType> GetInvoiceTypeAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default);
    Task<InvoiceType> GetCurrentInvoiceTypeAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default);

    Task<Company> GetCompanyAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default);
    IQueryable<BranchItemModel> GetBranchesBy(short? companyId = null);

    Task<Branch> GetBranchAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default);
    Task<Pos> GetPosAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default);

    IQueryable<BranchNotification> GetBranchNotificationsBy(short? branchId = null, short? invoiceTypeId = null);

    IQueryable<Invoice> GetInvoicesBy(short? typeId = null, short?[] processingStatuses = null);
    IQueryable<InvoiceItemModel> GetInvoicesForProcessing(short? typeId = null, short?[] processingStatuses = null);
    Task<Invoice> GetInvoiceAsync(long? id, bool tracking = false, bool includes = false, CancellationToken ct = default);

    IQueryable<InvoiceProcessingStatusLogItemModel> GetInvoiceProcessingStatusLogs(long? invoiceId);
    IQueryable<InvoiceProcessingLogItemModel> GetInvoiceProcessingLogs(long? invoiceId);
    IQueryable<InvoiceFileItemModel> GetInvoiceFiles(long? invoiceId);
    IQueryable<InvoiceNotificationItemModel> GetInvoiceNotifications(long? invoiceId);
}
