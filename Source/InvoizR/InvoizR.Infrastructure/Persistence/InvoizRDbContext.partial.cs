using InvoizR.Application.Specifications;
using InvoizR.Clients.DataContracts;
using InvoizR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Infrastructure.Persistence;

public partial class InvoizRDbContext
{
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

    public IQueryable<BranchNotification> GetBranchNotificationsBy(short? branchId = null, short? invoiceTypeId = null)
        => BranchNotification.Where(item => item.BranchId == branchId && item.InvoiceTypeId == invoiceTypeId);

    public IQueryable<Invoice> GetInvoicesBy(short? typeId = null, short?[] processingStatuses = null)
    {
        return Invoice
            .Include(e => e.Pos)
                .ThenInclude(e => e.Branch)
                .ThenInclude(e => e.Company)
            .Where(item => item.InvoiceTypeId == typeId && processingStatuses.Contains(item.ProcessingStatusId))
            ;
    }

    public IQueryable<InvoiceItemModel> GetInvoicesForProcessing(short? typeId = null, short?[] processingStatuses = null)
    {
        var query =
            from inv in Invoice
            where inv.InvoiceTypeId == typeId && processingStatuses.Contains(inv.ProcessingStatusId)
            select new InvoiceItemModel
            {
                Id = inv.Id,
                PosId = inv.PosId,
                CustomerName = inv.CustomerName,
                InvoiceTypeId = inv.InvoiceTypeId,
                InvoiceNumber = inv.InvoiceNumber,
                InvoiceDate = inv.InvoiceDate,
                InvoiceTotal = inv.InvoiceTotal,
                ProcessingStatusId = inv.ProcessingStatusId
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
