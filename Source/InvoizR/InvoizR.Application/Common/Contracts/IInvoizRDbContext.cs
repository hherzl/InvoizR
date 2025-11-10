using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Fallback;
using InvoizR.Clients.DataContracts.ThirdPartyServices;
using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace InvoizR.Application.Common.Contracts;

public interface IInvoizRDbContext : IDisposable
{
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task DispatchNotificationsAsync(CancellationToken cancellationToken = default);

    DbSet<ThirdPartyService> ThirdPartyService { get; set; }
    DbSet<ThirdPartyServiceParameter> ThirdPartyServiceParameter { get; set; }
    DbSet<EnumDescription> EnumDescription { get; set; }

    DbSet<Fallback> Fallback { get; set; }
    DbSet<FallbackProcessingLog> FallbackProcessingLog { get; set; }
    DbSet<FallbackFile> FallbackFile { get; set; }

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
    DbSet<InvoiceCancellationLog> InvoiceCancellationLog { get; set; }

    DbSet<VInvoiceProcessingStatus> VInvoiceProcessingStatus { get; set; }
    DbSet<VInvoiceProcessingType> VInvoiceProcessingType { get; set; }

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

    IQueryable<InvoiceItemModel> GetInvoicesForProcessing(short? typeId = null, short? processingTypeId = null, short?[] processingStatuses = null);
    Task<Invoice> GetInvoiceAsync(long? id, bool tracking = false, bool includes = false, CancellationToken ct = default);
    IQueryable<Invoice> GetInvoicesBy(short? fallbackId);

    IQueryable<InvoiceProcessingStatusLogItemModel> GetInvoiceProcessingStatusLogs(long? invoiceId);
    IQueryable<InvoiceProcessingLogItemModel> GetInvoiceProcessingLogs(long? invoiceId);
    IQueryable<InvoiceFileItemModel> GetInvoiceFiles(long? invoiceId);
    IQueryable<InvoiceNotificationItemModel> GetInvoiceNotifications(long? invoiceId);
}
