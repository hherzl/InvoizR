using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Invoices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.InvoiceTypes;

public class GetInvoiceTypesQueryHandler : IRequestHandler<GetInvoiceTypesQuery, ListResponse<InvoiceTypeItemModel>>
{
    private readonly IInvoizRDbContext _dbContext;

    public GetInvoiceTypesQueryHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ListResponse<InvoiceTypeItemModel>> Handle(GetInvoiceTypesQuery request, CancellationToken cancellationToken)
    {
        var query =
            from invoiceType in _dbContext.InvoiceTypes
            select new InvoiceTypeItemModel
            {
                Id = invoiceType.Id,
                Name = invoiceType.Name,
                SchemaType = invoiceType.SchemaType,
                SchemaVersion = invoiceType.SchemaVersion,
                Current = invoiceType.Current
            };

        return new(await query.ToListAsync(cancellationToken));
    }
}
