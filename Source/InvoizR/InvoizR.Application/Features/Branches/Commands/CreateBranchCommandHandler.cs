using InvoizR.Application.Common.Persistence;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Domain.Entities;
using MediatR;

namespace InvoizR.Application.Features.Branches.Commands;

public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, CreatedResponse<short?>>
{
    private readonly IInvoizRDbContext _dbContext;

    public CreateBranchCommandHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreatedResponse<short?>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var entity = new Branch
        {
            CompanyId = request.CompanyId,
            Name = request.Name,
            EstablishmentPrefix = request.EstablishmentPrefix,
            TaxAuthId = request.TaxAuthId,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email,
            Headquarters = request.Headquarters
        };

        if (request.HasLogo)
            entity.Logo = Convert.FromBase64String(request.Logo);

        _dbContext.Branch.Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(entity.Id);
    }
}
