using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Fallback;
using InvoizR.Clients.DataContracts.Invoices;
using InvoizR.Clients.DataContracts.ThirdPartyServices;
using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace InvoizR.Application.Common.Contracts;

public interface IInvoizRDbContext : IDisposable
{
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);

    Task DispatchNotificationsAsync(CancellationToken ct = default);

    DbSet<ThirdPartyService> ThirdPartyServices { get; set; }
    DbSet<ThirdPartyServiceParameter> ThirdPartyServiceParameters { get; set; }
    DbSet<EnumDescription> EnumDescriptions { get; set; }

    DbSet<Fallback> Fallbacks { get; set; }
    DbSet<FallbackProcessingLog> FallbackProcessingLogs { get; set; }
    DbSet<FallbackFile> FallbackFiles { get; set; }

    DbSet<Responsible> Responsibles { get; set; }
    DbSet<Company> Companies { get; set; }
    DbSet<Branch> Branches { get; set; }
    DbSet<BranchNotification> BranchNotifications { get; set; }
    DbSet<Pos> PointOfSales { get; set; }

    DbSet<InvoiceType> InvoiceTypes { get; set; }
    DbSet<Invoice> Invoices { get; set; }
    DbSet<InvoiceSyncStatusLog> InvoiceSyncStatusLogs { get; set; }
    DbSet<InvoiceSyncLog> InvoiceSyncLogs { get; set; }
    DbSet<InvoiceNotification> InvoiceNotifications { get; set; }
    DbSet<InvoiceFile> InvoiceFiles { get; set; }
    DbSet<InvoiceWebhookNotification> InvoiceWebhookNotifications { get; set; }
    DbSet<InvoiceCancellationLog> InvoiceCancellationLogs { get; set; }

    DbSet<VInvoiceSyncStatus> VInvoiceSyncStatuses { get; set; }
    DbSet<VInvoiceProcessingType> VInvoiceProcessingTypes { get; set; }

    IQueryable<ThirdPartyService> GetThirdPartyServices(string environmentId, bool tracking = false, bool includes = false);
    Task<ThirdPartyService> GetThirdPartyServiceAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default);
    IQueryable<ThirdPartyServiceParameterItemModel> GetThirdPartyServiceParameters(short? thirdPartyServiceId);

    Task<InvoiceType> GetInvoiceTypeAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default);
    Task<InvoiceType> GetCurrentInvoiceTypeAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default);

    Task<Company> GetCompanyAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default);

    Task<Fallback> GetCurrentFallbackAsync(short? companyId, bool tracking = false, bool includes = false, CancellationToken ct = default);
    Task<Fallback> GetFallbackAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default);
    Task<Fallback> GetFallbackByCompanyAndNameAsync(short? companyId, string name, bool tracking = false, bool includes = false, CancellationToken ct = default);
    IQueryable<FallbackInvoiceItemModel> GetInvoicesByFallback(short? fallbackId);
    IQueryable<FallbackProcessingLogItemModel> GetFallbackProcessingLogs(short? fallbackId);
    IQueryable<FallbackFileItemModel> GetFallbackFiles(short? fallbackId);

    IQueryable<BranchItemModel> GetBranchesBy(short? companyId = null);
    Task<Branch> GetBranchAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default);
    Task<Pos> GetPosAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default);
    IQueryable<PosItemModel> GetPosBy(short? branchId = null);

    Task<Responsible> GetResponsibleByCompanyIdAsync(short? companyId, bool tracking = false, bool includes = false, CancellationToken ct = default);
    IQueryable<ResponsibleItemModel> GetResponsiblesBy(short? companyId);

    IQueryable<BranchNotification> GetBranchNotificationsBy(short? branchId = null, short? invoiceTypeId = null);

    IQueryable<InvoiceItemModel> GetInvoicesForProcessing(short? typeId = null, short? processingTypeId = null, short?[] syncStatuses = null);
    Task<Invoice> GetInvoiceAsync(long? id, bool tracking = false, bool includes = false, CancellationToken ct = default);
    IQueryable<Invoice> GetInvoicesBy(short? fallbackId);

    IQueryable<InvoiceSyncStatusLogItemModel> GetInvoiceSyncStatusLogs(long? invoiceId);
    IQueryable<InvoiceSyncLogItemModel> GetInvoiceSyncLogs(long? invoiceId);
    IQueryable<InvoiceFileItemModel> GetInvoiceFilesBy(long? invoiceId);
    Task<InvoiceFile> GetInvoiceFileByAsync(long? invoiceId, string fileName, bool tracking = false, bool includes = false, CancellationToken ct = default);
    IQueryable<InvoiceNotificationItemModel> GetInvoiceNotifications(long? invoiceId);
}
