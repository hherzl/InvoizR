using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts;

public record GetCompaniesQuery : IRequest<ListResponse<CompanyItemModel>>
{
}
