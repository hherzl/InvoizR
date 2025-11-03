using InvoizR.Application.Specifications;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Fallback;
using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Infrastructure.Persistence;

public partial class InvoizRDbContext
{
    public IQueryable<ThirdPartyService> ThirdPartyServices(string environmentId, bool tracking = false, bool includes = false)
    {
        var query = ThirdPartyService.AsQueryable();

        if (tracking)
            query = query.AsNoTracking();

        if (includes)
            query = query.Include(e => e.ThirdPartyServiceParameters);

        return query.Where(item => item.EnvironmentId == environmentId);
    }

    public async Task<InvoiceType> GetInvoiceTypeAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = InvoiceType.AsQueryable();

        if (!tracking)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == id, ct);
    }

    public async Task<InvoiceType> GetCurrentInvoiceTypeAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = InvoiceType.AsQueryable();

        if (!tracking)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == id && item.Current == true, ct);
    }

    public async Task<Company> GetCompanyAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = Company.Specify(new GetCompanySpec(id));

        if (!tracking)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<Fallback> GetCurrentFallbackAsync(short? companyId, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = Fallback.Specify(new GetCurrentFallbackQuerySpec(companyId));

        if (!tracking)
            query = query.AsNoTracking();

        if (includes)
            query = query.Include(entity => entity.Company);

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<Fallback> GetFallbackAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = Fallback.Specify(new GetFallbackQuerySpec(id));

        if (!tracking)
            query = query.AsNoTracking();

        if (includes)
            query = query.Include(entity => entity.Company);

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<Fallback> GetFallbackByCompanyAndNameAsync(short? companyId, string name, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = Fallback.Where(item => item.CompanyId == companyId && item.Name == name);

        if (!tracking)
            query = query.AsNoTracking();

        if (includes)
            query = query.Include(entity => entity.Company);

        return await query.SingleOrDefaultAsync(ct);
    }

    public IQueryable<FallbackInvoiceItemModel> GetInvoicesByFallback(short? fallbackId)
    {
        var query =
            from invoice in Invoice
            join invoiceType in InvoiceType on invoice.InvoiceTypeId equals invoiceType.Id
            join pos in Pos on invoice.PosId equals pos.Id
            join branch in Branch on pos.BranchId equals branch.Id
            join company in Company on branch.CompanyId equals company.Id
            where invoice.FallbackId == fallbackId
            select new FallbackInvoiceItemModel
            {
                Id = invoice.Id,
                InvoiceTypeId = invoice.InvoiceTypeId,
                InvoiceType = invoiceType.Name,
                GenerationCode = invoice.GenerationCode,
                ControlNumber = invoice.ControlNumber
            };

        return query;
    }

    public IQueryable<FallbackProcessingLogItemModel> GetFallbackProcessingLogs(short? fallbackId)
    {
        var query =
           from fallbackProcessingLog in FallbackProcessingLog
           join vProcessingStatus in VInvoiceProcessingStatus on fallbackProcessingLog.SyncStatusId equals vProcessingStatus.Id
           where fallbackProcessingLog.FallbackId == fallbackId
           orderby fallbackProcessingLog.CreatedAt descending
           select new FallbackProcessingLogItemModel
           {
               CreatedAt = fallbackProcessingLog.CreatedAt,
               ProcessingStatusId = fallbackProcessingLog.SyncStatusId,
               ProcessingStatus = vProcessingStatus.Desc,
               LogType = fallbackProcessingLog.LogType,
               ContentType = fallbackProcessingLog.ContentType,
               Content = fallbackProcessingLog.Content
           };

        return query;
    }

    public IQueryable<FallbackFileItemModel> GetFallbackFiles(short? fallbackId)
    {
        var query =
            from fallbackFile in FallbackFile
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
            from branch in Branch
            join company in Company on branch.CompanyId equals company.Id
            where branch.CompanyId == companyId
            select new BranchItemModel
            {
                Id = branch.Id,
                CompanyId = branch.CompanyId,
                Company = company.Name,
                Name = branch.Name,
                EstablishmentPrefix = branch.EstablishmentPrefix,
                TaxAuthId = branch.TaxAuthId
            };

        return query;
    }

    public async Task<Branch> GetBranchAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = Branch.Specify(new GetBranchSpec(id));

        if (!tracking)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<Pos> GetPosAsync(short? id, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = Pos.Specify(new GetPosSpec(id));

        if (!tracking)
            query = query.AsNoTracking();

        if (includes)
            query = query.Include(e => e.Branch).ThenInclude(e => e.Company);

        return await query.SingleOrDefaultAsync(ct);
    }

    public IQueryable<PosItemModel> GetPosBy(short? branchId = null)
    {
        var query =
            from pos in Pos
            join branch in Branch on pos.BranchId equals branch.Id
            where pos.BranchId == branchId
            select new PosItemModel
            {
                Id = pos.Id,
                Name = pos.Name,
                BranchId = pos.BranchId,
                Branch = branch.Name,
                Code = pos.Code,
                TaxAuthPos = pos.TaxAuthPos,
                Description = pos.Description
            };

        return query;
    }

    public async Task<Responsible> GetResponsibleByCompanyIdAsync(short? companyId, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = Responsible.Specify(new GetResponsibleByCompanyIdSpec(companyId));

        if (!tracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(ct);
    }

    public IQueryable<BranchNotification> GetBranchNotificationsBy(short? branchId = null, short? invoiceTypeId = null)
        => BranchNotification.Where(item => item.BranchId == branchId && item.InvoiceTypeId == invoiceTypeId);

    public IQueryable<InvoiceItemModel> GetInvoicesForProcessing(short? typeId = null, short? processingTypeId = null, short?[] processingStatuses = null)
    {
        var query =
            from invoice in Invoice
            join pos in Pos on invoice.PosId equals pos.Id
            join branch in Branch on pos.BranchId equals branch.Id
            join company in Company on branch.CompanyId equals company.Id
            where invoice.InvoiceTypeId == typeId && invoice.ProcessingTypeId == processingTypeId && processingStatuses.Contains(invoice.ProcessingStatusId)
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
                ControlNumber = invoice.ControlNumber,
                ProcessingTypeId = invoice.ProcessingTypeId,
                SyncStatusId = invoice.ProcessingStatusId,
                Payload = invoice.Payload
            };

        return query;
    }

    public async Task<Invoice> GetInvoiceAsync(long? id, bool tracking = false, bool includes = false, CancellationToken ct = default)
    {
        var query = Invoice.Specify(new GetInvoiceSpec(id));

        if (!tracking)
            query = query.AsNoTracking();

        if (includes)
            query = query.Include(e => e.Pos).ThenInclude(e => e.Branch).ThenInclude(e => e.Company);

        return await query.SingleOrDefaultAsync(ct);
    }

    public IQueryable<Invoice> GetInvoicesBy(short? fallbackId)
    {
        return Invoice.Include(entity => entity.Pos).ThenInclude(entity => entity.Branch).ThenInclude(entity => entity.Company).Where(entity => entity.FallbackId == fallbackId);
    }

    public IQueryable<InvoiceProcessingStatusLogItemModel> GetInvoiceProcessingStatusLogs(long? invoiceId)
    {
        var query =
           from invProcessingStatusLog in InvoiceProcessingStatusLog
           join vInvProcessingStatus in VInvoiceProcessingStatus on invProcessingStatusLog.ProcessingStatusId equals vInvProcessingStatus.Id
           where invProcessingStatusLog.InvoiceId == invoiceId
           orderby invProcessingStatusLog.CreatedAt descending
           select new InvoiceProcessingStatusLogItemModel
           {
               InvoiceId = invProcessingStatusLog.Id,
               CreatedAt = invProcessingStatusLog.CreatedAt,
               ProcessingStatusId = invProcessingStatusLog.ProcessingStatusId,
               ProcessingStatus = vInvProcessingStatus.Desc
           };

        return query;
    }

    public IQueryable<InvoiceProcessingLogItemModel> GetInvoiceProcessingLogs(long? invoiceId)
    {
        var query =
            from invProcessingLog in InvoiceProcessingLog
            join vInvProcessingStatus in VInvoiceProcessingStatus on invProcessingLog.ProcessingStatusId equals vInvProcessingStatus.Id
            where invProcessingLog.InvoiceId == invoiceId
            orderby invProcessingLog.CreatedAt descending
            select new InvoiceProcessingLogItemModel
            {
                InvoiceId = invProcessingLog.Id,
                CreatedAt = invProcessingLog.CreatedAt,
                ProcessingStatusId = invProcessingLog.ProcessingStatusId,
                ProcessingStatus = vInvProcessingStatus.Desc,
                LogType = invProcessingLog.LogType,
                ContentType = invProcessingLog.ContentType,
                Content = invProcessingLog.Content
            };

        return query;
    }

    public IQueryable<InvoiceFileItemModel> GetInvoiceFiles(long? invoiceId)
    {
        var query =
            from invFile in InvoiceFile
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

    public IQueryable<InvoiceNotificationItemModel> GetInvoiceNotifications(long? invoiceId)
    {
        var query =
            from invNotification in InvoiceNotification
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
