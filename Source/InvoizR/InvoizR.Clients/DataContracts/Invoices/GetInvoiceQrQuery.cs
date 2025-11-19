using MediatR;

namespace InvoizR.Clients.DataContracts.Invoices;

public record GetInvoiceQrQuery : IRequest<InvoiceQrModel>
{
    public GetInvoiceQrQuery(long id)
    {
        Id = id;
    }

    public long Id { get; }
}
