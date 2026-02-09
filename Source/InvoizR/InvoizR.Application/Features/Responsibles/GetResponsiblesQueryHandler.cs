using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Responsibles;

public sealed class GetResponsiblesQueryHandler(IInvoizRDbContext dbContext) : IRequestHandler<GetResponsiblesQuery, ListResponse<ResponsibleItemModel>>
{
    public async Task<ListResponse<ResponsibleItemModel>> Handle(GetResponsiblesQuery request, CancellationToken ct)
    {
        var query =
            from responsible in dbContext.Responsibles
            orderby responsible.Name
            select new ResponsibleItemModel
            {
                Id = responsible.Id,
                Name = responsible.Name,
                Phone = responsible.Phone,
                Email = responsible.Email,
                IdType = responsible.IdType,
                IdNumber = responsible.IdNumber,
                AuthorizeCancellation = responsible.AuthorizeCancellation,
                AuthorizeFallback = responsible.AuthorizeFallback
            };

        if (request.CompanyId.HasValue)
            query = query.Where(item => item.CompanyId == request.CompanyId);

        return new(await query.ToListAsync(ct));
    }
}
