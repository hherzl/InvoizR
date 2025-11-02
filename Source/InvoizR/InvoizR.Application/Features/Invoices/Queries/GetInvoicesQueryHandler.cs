using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Specifications;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Invoices.Queries;

public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, PagedResponse<InvoiceItemModel>>
{
    private readonly IInvoizRDbContext _dbContext;

    public GetInvoicesQueryHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<InvoiceItemModel>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
    {
        var query =
            from inv in _dbContext.Invoice
            join branchPos in _dbContext.Pos on inv.PosId equals branchPos.Id
            join branch in _dbContext.Branch on branchPos.BranchId equals branch.Id
            join company in _dbContext.Company on branch.CompanyId equals company.Id
            join invType in _dbContext.InvoiceType on inv.InvoiceTypeId equals invType.Id
            join vInvProcessingStatus in _dbContext.VInvoiceProcessingStatus on inv.ProcessingStatusId equals vInvProcessingStatus.Id
            orderby inv.InvoiceDate descending
            select new InvoiceItemModel
            {
                Id = inv.Id,
                PosId = inv.PosId,
                Pos = branchPos.Name,
                BranchId = branchPos.BranchId,
                Branch = branch.Name,
                CompanyId = company.Id,
                Company = company.Name,
                Environment = company.Environment,
                CustomerName = inv.CustomerName,
                CustomerEmail = inv.CustomerEmail,
                InvoiceTypeId = inv.InvoiceTypeId,
                InvoiceType = invType.Name,
                InvoiceNumber = inv.InvoiceNumber,
                InvoiceDate = inv.InvoiceDate,
                InvoiceTotal = inv.InvoiceTotal,
                ControlNumber = inv.ControlNumber,
                ProcessingStatusId = inv.ProcessingStatusId,
                ProcessingStatus = vInvProcessingStatus.Desc
            };

        query = query
            .Specify(new GetInvoicesByPosSpec(request.PosId))
            .Specify(new GetInvoicesByProcessingStatusSpec(request.ProcessingStatusId))
            ;

        var itemsCount = await query.CountAsync(cancellationToken);

        var model = await query
            .Paging(request.PageSize, request.PageNumber)
            .ToListAsync(cancellationToken)
            ;

        return new(model, request.PageSize, request.PageNumber, itemsCount);
    }
}
