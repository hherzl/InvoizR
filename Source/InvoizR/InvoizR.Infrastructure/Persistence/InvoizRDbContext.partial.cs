using InvoizR.Application.QuerySpecs;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Fallback;
using InvoizR.Clients.DataContracts.Invoices;
using InvoizR.Clients.DataContracts.ThirdPartyServices;
using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Infrastructure.Persistence;

public partial class InvoizRDbContext
{
    public IQueryable<ThirdPartyService> GetThirdPartyServices(string environmentId, bool tracking = false, bool includes = false)
    {
        var query = ThirdPartyServices.AsQueryable();

        if (tracking)
            query = query.AsNoTracking();

        if (includes)
            query = query.Include(e => e.ThirdPartyServiceParameters);

        return query.Where(item => item.EnvironmentId == environmentId);
    }

    public async Task<ThirdPartyService> GetThirdPartyServiceAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = ThirdPartyServices.AsQueryable();

        if (!tracking)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == id, ct);
    }

    public IQueryable<ThirdPartyServiceParameterItemModel> GetThirdPartyServiceParameters(short? thirdPartyServiceId)
    {
        var query =
            from thirdPartyServiceParameter in ThirdPartyServiceParameters
            where thirdPartyServiceParameter.ThirdPartyServiceId == thirdPartyServiceId
            select new ThirdPartyServiceParameterItemModel
            {
                Id = thirdPartyServiceParameter.Id,
                Category = thirdPartyServiceParameter.Category,
                Name = thirdPartyServiceParameter.Name,
                DefaultValue = thirdPartyServiceParameter.DefaultValue,
                RequiresEncryption = thirdPartyServiceParameter.RequiresEncryption
            };

        return query;
    }

    public async Task<InvoiceType> GetInvoiceTypeAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = InvoiceTypes.AsQueryable();

        if (!tracking)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == id, ct);
    }

    public async Task<InvoiceType> GetCurrentInvoiceTypeAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = InvoiceTypes.AsQueryable();

        if (!tracking)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == id && item.Current == true, ct);
    }

    public async Task<Company> GetCompanyAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = Companies.AddQuerySpec(new GetCompanyQuerySpec(id));

        if (!tracking)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<Fallback> GetCurrentFallbackAsync(short? companyId, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = Fallbacks.AddQuerySpec(new GetCurrentFallbackQuerySpec(companyId));

        if (!tracking)
            query = query.AsNoTracking();

        if (includes)
            query = query.Include(entity => entity.Company);

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<Fallback> GetFallbackAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = Fallbacks.AddQuerySpec(new GetFallbackQuerySpec(id));

        if (!tracking)
            query = query.AsNoTracking();

        if (includes)
            query = query.Include(entity => entity.Company);

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<Fallback> GetFallbackByCompanyAndNameAsync(short? companyId, string name, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = Fallbacks.Where(item => item.CompanyId == companyId && item.Name == name);

        if (!tracking)
            query = query.AsNoTracking();

        if (includes)
            query = query.Include(entity => entity.Company);

        return await query.SingleOrDefaultAsync(ct);
    }

    public IQueryable<FallbackInvoiceItemModel> GetInvoicesByFallback(short? fallbackId)
    {
        var query =
            from invoice in Invoices
            join invoiceType in InvoiceTypes on invoice.InvoiceTypeId equals invoiceType.Id
            join pos in PointOfSales on invoice.PosId equals pos.Id
            join branch in Branches on pos.BranchId equals branch.Id
            join company in Companies on branch.CompanyId equals company.Id
            where invoice.FallbackId == fallbackId
            select new FallbackInvoiceItemModel
            {
                Id = invoice.Id,
                InvoiceTypeId = invoice.InvoiceTypeId,
                InvoiceType = invoiceType.Name,
                InvoiceGuid = invoice.InvoiceGuid,
                AuditNumber = invoice.AuditNumber
            };

        return query;
    }

    public IQueryable<FallbackProcessingLogItemModel> GetFallbackProcessingLogs(short? fallbackId)
    {
        var query =
           from fallbackProcessingLog in FallbackProcessingLogs
           join vSyncStatus in VInvoiceSyncStatuses on fallbackProcessingLog.SyncStatusId equals vSyncStatus.Id
           where fallbackProcessingLog.FallbackId == fallbackId
           orderby fallbackProcessingLog.CreatedAt descending
           select new FallbackProcessingLogItemModel
           {
               CreatedAt = fallbackProcessingLog.CreatedAt,
               SyncStatusId = fallbackProcessingLog.SyncStatusId,
               SyncStatus = vSyncStatus.Desc,
               LogType = fallbackProcessingLog.LogType,
               ContentType = fallbackProcessingLog.ContentType,
               Content = fallbackProcessingLog.Content
           };

        return query;
    }

    public IQueryable<FallbackFileItemModel> GetFallbackFiles(short? fallbackId)
    {
        var query =
            from fallbackFile in FallbackFiles
            where fallbackFile.FallbackId == fallbackId
            select new FallbackFileItemModel
            {
                Size = fallbackFile.Size,
                MimeType = fallbackFile.MimeType,
                FileType = fallbackFile.FileType,
                FileName = fallbackFile.FileName,
                ExternalUrl = fallbackFile.ExternalUrl,
                CreatedAt = fallbackFile.CreatedAt,
                File = fallbackFile.File
            };

        return query;
    }

    public IQueryable<BranchItemModel> GetBranchesBy(short? companyId = null)
    {
        var query =
            from branch in Branches
            join company in Companies on branch.CompanyId equals company.Id
            where branch.CompanyId == companyId
            select new BranchItemModel
            {
                Id = branch.Id,
                CompanyId = branch.CompanyId,
                Company = company.Name,
                Name = branch.Name,
                TaxAuthId = branch.TaxAuthId
            };

        return query;
    }

    public async Task<Branch> GetBranchAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = Branches.AddQuerySpec(new GetBranchQuerySpec(id));

        if (!tracking)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<Pos> GetPosAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = PointOfSales.AddQuerySpec(new GetPosQuerySpec(id));

        if (!tracking)
            query = query.AsNoTracking();

        if (includes)
            query = query.Include(e => e.Branch).ThenInclude(e => e.Company);

        return await query.SingleOrDefaultAsync(ct);
    }

    public IQueryable<PosItemModel> GetPosBy(short? branchId = null)
    {
        var query =
            from pos in PointOfSales
            join branch in Branches on pos.BranchId equals branch.Id
            where pos.BranchId == branchId
            select new PosItemModel
            {
                Id = pos.Id,
                Name = pos.Name,
                BranchId = pos.BranchId,
                Branch = branch.Name,
                Code = pos.Code,
                TaxAuthId = pos.TaxAuthId,
                Description = pos.Description
            };

        return query;
    }

    public async Task<Responsible> GetResponsibleByCompanyIdAsync(short? companyId, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = Responsibles.AddQuerySpec(new GetResponsibleByCompanyIdQuerySpec(companyId));

        if (!tracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(ct);
    }

    public IQueryable<ResponsibleItemModel> GetResponsiblesBy(short? companyId)
    {
        var query =
            from responsible in Responsibles
            where responsible.CompanyId == companyId
            orderby responsible.Name
            select new ResponsibleItemModel
            {
                Id = responsible.Id,
                Name = responsible.Name,
                Phone = responsible.Phone,
                Email = responsible.Email,
                IdType = responsible.IdType,
                IdNumber = responsible.IdNumber,
                AuthorizeCancellation = responsible.AuthorizeCancellation,
                AuthorizeFallback = responsible.AuthorizeFallback
            };

        return query;
    }

    public IQueryable<BranchNotification> GetBranchNotificationsBy(short? branchId = null, short? invoiceTypeId = null)
        => BranchNotifications.Where(item => item.BranchId == branchId && item.InvoiceTypeId == invoiceTypeId);

    public IQueryable<InvoiceItemModel> GetInvoicesForProcessing(short? typeId = null, short? processingTypeId = null, short?[] syncStatuses = null)
    {
        var query =
            from invoice in Invoices
            join pos in PointOfSales on invoice.PosId equals pos.Id
            join branch in Branches on pos.BranchId equals branch.Id
            join company in Companies on branch.CompanyId equals company.Id
            where invoice.InvoiceTypeId == typeId && invoice.ProcessingTypeId == processingTypeId && syncStatuses.Contains(invoice.SyncStatusId)
            select new InvoiceItemModel
            {
                Id = invoice.Id,
                PosId = invoice.PosId,
                BranchId = branch.Id,
                CompanyId = company.Id,
                Environment = company.Environment,
                CustomerName = invoice.CustomerName,
                InvoiceTypeId = invoice.InvoiceTypeId,
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceDate = invoice.InvoiceDate,
                InvoiceTotal = invoice.InvoiceTotal,
                AuditNumber = invoice.AuditNumber,
                ProcessingTypeId = invoice.ProcessingTypeId,
                SyncStatusId = invoice.SyncStatusId,
                Payload = invoice.Payload
            };

        return query;
    }

    public async Task<Invoice> GetInvoiceAsync(long? id, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = Invoices.AddQuerySpec(new GetInvoiceQuerySpec(id));

        if (!tracking)
            query = query.AsNoTracking();

        if (includes)
            query = query.Include(e => e.Pos).ThenInclude(e => e.Branch).ThenInclude(e => e.Company);

        return await query.SingleOrDefaultAsync(ct);
    }

    public IQueryable<Invoice> GetInvoicesBy(short? fallbackId)
    {
        return Invoices.Include(entity => entity.Pos).ThenInclude(entity => entity.Branch).ThenInclude(entity => entity.Company).Where(entity => entity.FallbackId == fallbackId);
    }

    public IQueryable<InvoiceSyncStatusLogItemModel> GetInvoiceSyncStatusLogs(long? invoiceId)
    {
        var query =
           from invSyncStatusLog in InvoiceSyncStatusLogs
           join vInvSyncStatus in VInvoiceSyncStatuses on invSyncStatusLog.SyncStatusId equals vInvSyncStatus.Id
           where invSyncStatusLog.InvoiceId == invoiceId
           orderby invSyncStatusLog.CreatedAt descending
           select new InvoiceSyncStatusLogItemModel
           {
               InvoiceId = invSyncStatusLog.Id,
               CreatedAt = invSyncStatusLog.CreatedAt,
               SyncStatusId = invSyncStatusLog.SyncStatusId,
               SyncStatus = vInvSyncStatus.Desc
           };

        return query;
    }

    public IQueryable<InvoiceSyncLogItemModel> GetInvoiceSyncLogs(long? invoiceId)
    {
        var query =
            from invSyncLog in InvoiceSyncLogs
            join vInvSyncStatus in VInvoiceSyncStatuses on invSyncLog.SyncStatusId equals vInvSyncStatus.Id
            where invSyncLog.InvoiceId == invoiceId
            orderby invSyncLog.CreatedAt descending
            select new InvoiceSyncLogItemModel
            {
                InvoiceId = invSyncLog.Id,
                CreatedAt = invSyncLog.CreatedAt,
                SyncStatusId = invSyncLog.SyncStatusId,
                SyncStatus = vInvSyncStatus.Desc,
                LogType = invSyncLog.LogType,
                ContentType = invSyncLog.ContentType,
                Content = invSyncLog.Content
            };

        return query;
    }

    public IQueryable<InvoiceFileItemModel> GetInvoiceFilesBy(long? invoiceId)
    {
        var query =
            from invFile in InvoiceFiles
            where invFile.InvoiceId == invoiceId
            select new InvoiceFileItemModel
            {
                Size = invFile.Size,
                MimeType = invFile.MimeType,
                FileType = invFile.FileType,
                FileName = invFile.FileName,
                ExternalUrl = invFile.ExternalUrl,
                CreatedAt = invFile.CreatedAt,
                File = invFile.File
            };

        return query;
    }

    public async Task<InvoiceFile> GetInvoiceFileByAsync(long? invoiceId, string fileName, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = InvoiceFiles.AsQueryable();

        if (tracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(item => item.InvoiceId == invoiceId && item.FileName == fileName, ct);
    }

    public IQueryable<InvoiceNotificationItemModel> GetInvoiceNotifications(long? invoiceId)
    {
        var query =
            from invNotification in InvoiceNotifications
            where invNotification.InvoiceId == invoiceId
            select new InvoiceNotificationItemModel
            {
                Email = invNotification.Email,
                Bcc = invNotification.Bcc,
                Files = invNotification.Files,
                Successful = invNotification.Successful,
                CreatedAt = invNotification.CreatedAt
            };

        return query;
    }
}
