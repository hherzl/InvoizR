using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts.Invoices;

public record GetInvoiceQuery : IRequest<SingleResponse<InvoiceDetailsModel>>
{
    public GetInvoiceQuery(long id)
    {
        Id = id;
    }

    public long? Id { get; }
}
