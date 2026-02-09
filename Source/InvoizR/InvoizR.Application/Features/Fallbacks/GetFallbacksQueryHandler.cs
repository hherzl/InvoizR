using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Fallback;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Fallbacks;

public sealed class GetFallbacksQueryHandler(IInvoizRDbContext dbContext) : IRequestHandler<GetFallbacksQuery, PagedResponse<FallbackItemModel>>
{
    public async Task<PagedResponse<FallbackItemModel>> Handle(GetFallbacksQuery request, CancellationToken cancellationToken)
    {
        var query =
            from fallback in dbContext.Fallbacks
            join company in dbContext.Companies on fallback.CompanyId equals company.Id
            select new FallbackItemModel
            {
                Id = fallback.Id,
                Company = company.Name,
                CompanyId = fallback.CompanyId,
                Name = fallback.Name,
                StartDateTime = fallback.StartDateTime,
                EndDateTime = fallback.EndDateTime,
                Enable = fallback.Enable,
                FallbackGuid = fallback.FallbackGuid,
                SyncStatusId = fallback.SyncStatusId
            };

        var itemsCount = await query.CountAsync(cancellationToken);

        var model = await query
            .Paging(request.PageSize, request.PageNumber)
            .ToListAsync(cancellationToken)
            ;

        return new(model, request.PageSize, request.PageNumber, itemsCount);
    }
}
