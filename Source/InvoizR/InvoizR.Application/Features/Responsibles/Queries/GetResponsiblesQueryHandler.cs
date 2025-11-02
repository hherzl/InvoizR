using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Responsibles.Queries;

public class GetResponsiblesQueryHandler : IRequestHandler<GetResponsiblesQuery, ListResponse<ResponsibleItemModel>>
{
    private readonly IInvoizRDbContext _dbContext;

    public GetResponsiblesQueryHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ListResponse<ResponsibleItemModel>> Handle(GetResponsiblesQuery request, CancellationToken cancellationToken)
    {
        var query =
            from responsible in _dbContext.Responsible
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

        return new(await query.ToListAsync(cancellationToken));
    }
}
