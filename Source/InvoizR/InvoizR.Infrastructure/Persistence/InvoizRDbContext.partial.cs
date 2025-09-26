﻿using InvoizR.Application.Specifications;
using InvoizR.Clients.DataContracts;
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

    public IQueryable<BranchNotification> GetBranchNotificationsBy(short? branchId = null, short? invoiceTypeId = null)
        => BranchNotification.Where(item => item.BranchId == branchId && item.InvoiceTypeId == invoiceTypeId);

    public IQueryable<InvoiceItemModel> GetInvoicesForProcessing(short? typeId = null, short? processingTypeId = null, short?[] processingStatuses = null)
    {
        var query =
            from inv in Invoice
            join pos in Pos on inv.PosId equals pos.Id
            join branch in Branch on pos.BranchId equals branch.Id
            join company in Company on branch.CompanyId equals company.Id
            where inv.InvoiceTypeId == typeId && inv.ProcessingTypeId == processingTypeId && processingStatuses.Contains(inv.ProcessingStatusId)
            select new InvoiceItemModel
            {
                Id = inv.Id,
                PosId = inv.PosId,
                Environment = company.Environment,
                CustomerName = inv.CustomerName,
                InvoiceTypeId = inv.InvoiceTypeId,
                InvoiceNumber = inv.InvoiceNumber,
                InvoiceDate = inv.InvoiceDate,
                InvoiceTotal = inv.InvoiceTotal,
                ControlNumber = inv.ControlNumber,
                ProcessingStatusId = inv.ProcessingStatusId,
                Payload = inv.Payload
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
