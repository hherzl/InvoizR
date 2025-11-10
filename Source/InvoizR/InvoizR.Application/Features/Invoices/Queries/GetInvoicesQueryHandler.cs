using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.QuerySpecs;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Invoices.Queries;

public sealed class GetInvoicesQueryHandler(IInvoizRDbContext dbContext) : IRequestHandler<GetInvoicesQuery, PagedResponse<InvoiceItemModel>>
{
    public async Task<PagedResponse<InvoiceItemModel>> Handle(GetInvoicesQuery request, CancellationToken ct)
    {
        var query =
            from invoice in dbContext.Invoice
            join pos in dbContext.Pos on invoice.PosId equals pos.Id
            join branch in dbContext.Branch on pos.BranchId equals branch.Id
            join company in dbContext.Company on branch.CompanyId equals company.Id
            join invoiceType in dbContext.InvoiceType on invoice.InvoiceTypeId equals invoiceType.Id
            join vProcessingType in dbContext.VInvoiceProcessingType on invoice.ProcessingTypeId equals vProcessingType.Id
            join vInvoiceSyncStatus in dbContext.VInvoiceProcessingStatus on invoice.ProcessingStatusId equals vInvoiceSyncStatus.Id
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
                SyncStatusId = invoice.ProcessingStatusId,
                SyncStatus = vInvoiceSyncStatus.Desc
            };

        query = query
            .Spec(new GetInvoicesByPosQuerySpec(request.PosId))
            .Spec(new GetInvoicesByInvoiceTypeQuerySpec(request.InvoiceTypeId))
            .Spec(new GetInvoicesByProcessingTypeQuerySpec(request.ProcessingTypeId))
            .Spec(new GetInvoicesBySyncStatusQuerySpec(request.SyncStatusId))
            ;

        var itemsCount = await query.CountAsync(ct);

        var model = await query
            .Paging(request.PageSize, request.PageNumber)
            .ToListAsync(ct)
            ;

        return new(model, request.PageSize, request.PageNumber, itemsCount);
    }
}
