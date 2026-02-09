using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Invoices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Invoices;

public class GetInvoicesViewBagQueryHandler(IInvoizRDbContext dbContext) : IRequestHandler<GetInvoicesViewBagQuery, GetInvoicesViewBagResponse>
{
    public async Task<GetInvoicesViewBagResponse> Handle(GetInvoicesViewBagQuery request, CancellationToken st)
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
            BranchPos = await dbContext.PointOfSales.Include(e => e.Branch).OrderBy(e => e.Branch.Name).Select(item => new ListItem<short?>(item.Id, $"{item.Branch.Company.Name}/{item.Branch.Name}/{item.Name}")).ToListAsync(st),
            InvoiceTypes = await dbContext.InvoiceTypes.OrderBy(e => e.Name).Select(item => new ListItem<short?>(item.Id, item.Name)).ToListAsync(st),
            ProcessingTypes = await dbContext.VInvoiceProcessingTypes.Select(item => new ListItem<short?>(item.Id, item.Desc)).ToListAsync(st),
            SyncStatuses = await dbContext.VInvoiceSyncStatuses.Select(item => new ListItem<short?>(item.Id, item.Desc)).ToListAsync(st)
        };
    }
}
