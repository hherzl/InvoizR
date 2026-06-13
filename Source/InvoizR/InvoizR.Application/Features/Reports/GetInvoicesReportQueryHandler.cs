using InvoizR.Application.Common.Contracts;
using InvoizR.Application.QuerySpecs;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Reports;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Reports;

public sealed class GetInvoicesReportQueryHandler(IInvoizRDbContext dbContext) : IRequestHandler<GetInvoicesReportQuery, ListResponse<InvoiceItemReportModel>>
{
    public async Task<ListResponse<InvoiceItemReportModel>> Handle(GetInvoicesReportQuery request, CancellationToken ct)
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
            select new InvoiceItemReportModel
            {
                Id = invoice.Id,
                PosId = invoice.PosId,
                Pos = pos.Name,
                BranchId = pos.BranchId,
                Branch = branch.Name,
                CompanyId = company.Id,
                Company = company.Name,
                Environment = company.Environment,
                CustomerId = invoice.CustomerId,
                CustomerName = invoice.CustomerName,
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

        query = query.AddQuerySpec(new GetInvoicesReportByInvoiceTypeQuerySpec(request.InvoiceTypeId))
            .AddQuerySpec(new GetInvoicesReportByDatesQuerySpec(request.StartDate, request.EndDate))
            .AddQuerySpec(new GetInvoicesReportByPosQuerySpec(request.BranchId))
            .AddQuerySpec(new GetInvoicesReportByBranchQuerySpec(request.BranchId));

        return new(await query.ToListAsync(ct));
    }
}
