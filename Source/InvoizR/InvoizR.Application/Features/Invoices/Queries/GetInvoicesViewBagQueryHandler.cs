using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Invoices.Queries;

public class GetInvoicesViewBagQueryHandler : IRequestHandler<GetInvoicesViewBagQuery, GetInvoicesViewBagResponse>
{
    private readonly IInvoizRDbContext _dbContext;

    public GetInvoicesViewBagQueryHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetInvoicesViewBagResponse> Handle(GetInvoicesViewBagQuery request, CancellationToken cancellationToken)
    {
        var pageSizes = new List<ListItem<int>>
        {
            new(10, "10"),
            new(25, "25"),
            new(50, "50"),
            new(100, "100")
        };

        var branchPos = await _dbContext.Pos
            .Include(e => e.Branch)
            .OrderBy(e => e.Branch.Name)
            .Select(item => new ListItem<short?>(item.Id, $"{item.Branch.Name}/{item.Name}"))
            .ToListAsync(cancellationToken);

        var processingStatuses = await _dbContext.VInvoiceProcessingStatus
            .Select(item => new ListItem<short?>(item.Id, item.Desc))
            .ToListAsync(cancellationToken);

        return new()
        {
            PageSizes = pageSizes,
            BranchPos = branchPos,
            ProcessingStatuses = processingStatuses
        };
    }
}
