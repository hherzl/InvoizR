using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Application.Features.InvoiceTypes;

public class SetInvoiceTypeAsCurrentCommandHandler : IRequestHandler<SetInvoiceTypeAsCurrentCommand, Response>
{
    private readonly IInvoizRDbContext _dbContext;

    public SetInvoiceTypeAsCurrentCommandHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(SetInvoiceTypeAsCurrentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.GetInvoiceTypeAsync(request.Id, tracking: true, ct: cancellationToken);
        if (entity == null)
            return null;

        if (entity.Current == true)
            return new();

        entity.Current = true;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new();
    }
}
