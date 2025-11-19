using MediatR;

namespace InvoizR.Clients.DataContracts.Invoices;

public record GetInvoicesViewBagQuery : IRequest<GetInvoicesViewBagResponse>
{
}
