using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.QuerySpecs;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Invoices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Invoices;

public sealed class GetInvoicesQueryHandler(IInvoizRDbContext dbContext) : IRequestHandler<GetInvoicesQuery, PagedResponse<InvoiceItemModel>>
{
    public async Task<PagedResponse<InvoiceItemModel>> Handle(GetInvoicesQuery request, CancellationToken ct = default)
    {
        var query =
            from invoice in dbContext.Invoices
            join pos in dbContext.PointOfSales on invoice.PosId equals pos.Id
            join branch in dbContext.Branches on pos.BranchId equals branch.Id
            join company in dbContext.Companies on branch.CompanyId equals company.Id
            join invoiceType in dbContext.InvoiceTypes on invoice.InvoiceTypeId equals invoiceType.Id
            join vProcessingType in dbContext.VInvoiceProcessingTypes on invoice.ProcessingTypeId equals vProcessingType.Id
            join vInvoiceSyncStatus in dbContext.VInvoiceSyncStatuses on invoice.SyncStatusId equals vInvoiceSyncStatus.Id
            orderby invoice.InvoiceDate descending
            select new InvoiceItemModel
            {
                Id = invoice.Id,
                PosId = invoice.PosId,
                Pos = pos.Name,
                BranchId = pos.BranchId,
                Branch = branch.Name,
                CompanyId = company.Id,
                Company = company.Name,
                Environment = company.Environment,
                CustomerName = invoice.CustomerName,
                CustomerEmail = invoice.CustomerEmail,
                InvoiceTypeId = invoice.InvoiceTypeId,
                InvoiceType = invoiceType.Name,
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceDate = invoice.InvoiceDate,
                InvoiceTotal = invoice.InvoiceTotal,
                AuditNumber = invoice.AuditNumber,
                ProcessingTypeId = invoice.ProcessingTypeId,
                ProcessingType = vProcessingType.Desc,
                SyncStatusId = invoice.SyncStatusId,
                SyncStatus = vInvoiceSyncStatus.Desc
            };

        query = query
            .AddQuerySpec(new GetInvoicesByPosQuerySpec(request.PosId))
            .AddQuerySpec(new GetInvoicesByInvoiceTypeQuerySpec(request.InvoiceTypeId))
            .AddQuerySpec(new GetInvoicesByProcessingTypeQuerySpec(request.ProcessingTypeId))
            .AddQuerySpec(new GetInvoicesBySyncStatusQuerySpec(request.SyncStatusId))
            ;

        var itemsCount = await query.CountAsync(ct);

        var model = await query
            .Paging(request.PageSize, request.PageNumber)
            .ToListAsync(ct)
            ;

        return new(model, request.PageSize, request.PageNumber, itemsCount);
    }
}
