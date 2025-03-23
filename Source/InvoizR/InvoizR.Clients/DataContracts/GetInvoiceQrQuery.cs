using MediatR;

namespace InvoizR.Clients.DataContracts;

public record GetInvoiceQrQuery : IRequest<InvoiceQrModel>
{
    public GetInvoiceQrQuery(long id)
    {
        Id = id;
    }

    public long Id { get; }
}

public record InvoiceQrModel
{
    public byte[] Qr { get; set; }
    public string ContentType { get; set; }
    public string ControlNumber { get; set; }
}
