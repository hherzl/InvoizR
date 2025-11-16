using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Domain.Entities;
using MediatR;

namespace InvoizR.Application.Features.Branches.Commands;

public sealed class CreateBranchCommandHandler(IInvoizRDbContext dbContext) : IRequestHandler<CreateBranchCommand, CreatedResponse<short?>>
{
    public async Task<CreatedResponse<short?>> Handle(CreateBranchCommand request, CancellationToken ct)
    {
        var entity = new Branch
        {
            CompanyId = request.CompanyId,
            Name = request.Name,
            TaxAuthId = request.TaxAuthId,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email,
            Headquarters = request.Headquarters,
            NonCustomerEmail = request.NonCustomerEmail
        };

        if (request.HasLogo)
            entity.Logo = Convert.FromBase64String(request.Logo);

        dbContext.Branch.Add(entity);

        await dbContext.SaveChangesAsync(ct);

        return new(entity.Id);
    }
}
