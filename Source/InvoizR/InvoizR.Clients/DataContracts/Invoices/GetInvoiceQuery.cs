using MediatR;

namespace InvoizR.Clients.DataContracts.Invoices;

public record GetInvoiceQuery : IRequest<InvoiceDetailsModel>
{
    public GetInvoiceQuery(long id)
    {
        Id = id;
    }

    public long? Id { get; }
}
