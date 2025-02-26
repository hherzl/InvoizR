using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts;

public record GetInvoiceTypesQuery : IRequest<ListResponse<InvoiceTypeItemModel>>
{
}
