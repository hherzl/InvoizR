using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Domain.Entities;
using MediatR;

namespace InvoizR.Application.Features.Responsibles.Commands;

public class CreateResponsibleCommandHandler : IRequestHandler<CreateResponsibleCommand, CreatedResponse<short?>>
{
    private readonly IInvoizRDbContext _dbContext;

    public CreateResponsibleCommandHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreatedResponse<short?>> Handle(CreateResponsibleCommand request, CancellationToken cancellationToken)
    {
        var entity = new Responsible
        {
            CompanyId = request.CompanyId,
            Name = request.Name,
            Phone = request.Phone,
            Email = request.Email,
            IdType = request.IdType,
            IdNumber = request.IdNumber,
            AuthorizeCancellation = request.AuthorizeCancellation,
            AuthorizeFallback = request.AuthorizeFallback
        };

        _dbContext.Responsible.Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(entity.Id);
    }
}
