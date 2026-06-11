using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Invoices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Invoices;

public sealed class GetInvoicesViewBagQueryHandler(IInvoizRDbContext dbContext) : IRequestHandler<GetInvoicesViewBagQuery, GetInvoicesViewBagResponse>
{
    public async Task<GetInvoicesViewBagResponse> Handle(GetInvoicesViewBagQuery request, CancellationToken ct)
    {
        var pageSizes = new List<ListItem<int>>
        {
            new(10, "10"),
            new(25, "25"),
            new(50, "50"),
            new(100, "100")
        };

        return new()
        {
            PageSizes = pageSizes,
            BranchPos = await dbContext.PointOfSales.Include(e => e.Branch).OrderBy(e => e.Branch.Name).Select(item => new ListItem<short?>(item.Id, $"{item.Branch.Company.Name}/{item.Branch.Name}/{item.Name}")).ToListAsync(ct),
            InvoiceTypes = await dbContext.InvoiceTypes.OrderBy(e => e.Name).Select(item => new ListItem<short?>(item.Id, item.Name)).ToListAsync(ct),
            ProcessingTypes = await dbContext.VInvoiceProcessingTypes.Select(item => new ListItem<short?>(item.Id, item.Desc)).ToListAsync(ct),
            SyncStatuses = await dbContext.VInvoiceSyncStatuses.Select(item => new ListItem<short?>(item.Id, item.Desc)).ToListAsync(ct)
        };
    }
}
