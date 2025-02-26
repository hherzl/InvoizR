using MediatR;

namespace InvoizR.Clients.DataContracts;

public record GetInvoicesViewBagQuery : IRequest<GetInvoicesViewBagResponse>
{
}
