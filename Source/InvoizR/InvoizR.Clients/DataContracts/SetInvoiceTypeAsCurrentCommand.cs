using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts;

public record SetInvoiceTypeAsCurrentCommand : IRequest<Response>
{
    public SetInvoiceTypeAsCurrentCommand(short? id)
    {
        Id = id;
    }

    public short? Id { get; }
}
