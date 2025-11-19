using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts.Invoices;

public record GetInvoiceTypesQuery : IRequest<ListResponse<InvoiceTypeItemModel>>
{
}
