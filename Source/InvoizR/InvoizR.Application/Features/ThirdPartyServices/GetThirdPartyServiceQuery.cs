using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.ThirdPartyServices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.ThirdPartyServices;

public sealed class GetThirdPartyServiceQueryHandler(IInvoizRDbContext dbContext) : IRequestHandler<GetThirdPartyServiceQuery, SingleResponse<ThirdPartyServiceDetailsModel>>
{
    public async Task<SingleResponse<ThirdPartyServiceDetailsModel>> Handle(GetThirdPartyServiceQuery request, CancellationToken st)
    {
        var entity = await dbContext.GetThirdPartyServiceAsync(request.Id, includes: true, ct: st);
        if (entity == null)
            return null;

        var paremeters = await dbContext.GetThirdPartyServiceParameters(entity.Id).ToListAsync(st);

        var model = new ThirdPartyServiceDetailsModel
        {
            Id = entity.Id,
            EnvironmentId = entity.EnvironmentId,
            Name = entity.Name,
            Description = entity.Description,
            Parameters = paremeters
        };

        return new(model);
    }
}
