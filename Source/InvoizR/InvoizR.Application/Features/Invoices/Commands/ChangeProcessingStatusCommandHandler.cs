using InvoizR.Application.Common.Persistence;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using MediatR;

namespace InvoizR.Application.Features.Invoices.Commands;

internal class ChangeProcessingStatusCommandHandler : IRequestHandler<ChangeProcessingStatusCommand, Response>
{
    private readonly IInvoizRDbContext _dbContext;

    public ChangeProcessingStatusCommandHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(ChangeProcessingStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.GetInvoiceAsync(request.InvoiceId, ct: cancellationToken);
        if (entity == null)
            return null;

        if (entity.ProcessingStatusId >= (short)InvoiceProcessingStatus.Processed)
            throw new InvoizRException($"Change processing status for '{entity.Id}' ({entity.InvoiceNumber}) invoice can't be completed.");

        entity.ProcessingStatusId = (short)InvoiceProcessingStatus.Requested;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new();
    }
}
