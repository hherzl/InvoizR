using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.ThirdPartyServices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.ThirdPartyServices.Queries;

public sealed class GetThirdPartyServicesQueryHandler(IInvoizRDbContext dbContext) : IRequestHandler<GetThirdPartyServicesQuery, ListResponse<ThirdPartyServiceItemModel>>
{
    public async Task<ListResponse<ThirdPartyServiceItemModel>> Handle(GetThirdPartyServicesQuery request, CancellationToken cancellationToken)
    {
        var query =
            from thirdPartyService in dbContext.ThirdPartyService
            orderby thirdPartyService.Name ascending
            select new ThirdPartyServiceItemModel
            {
                Id = thirdPartyService.Id,
                Name = thirdPartyService.Name,
                EnvironmentId = thirdPartyService.EnvironmentId
            };

        var model = await query.ToListAsync(cancellationToken);

        return new(model);
    }
}
