using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using MediatR;

namespace InvoizR.Application.Features.Invoices.Commands;

public sealed class ChangeProcessingStatusCommandHandler(IInvoizRDbContext dbContext) : IRequestHandler<ChangeProcessingStatusCommand, Response>
{
    public async Task<Response> Handle(ChangeProcessingStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.GetInvoiceAsync(request.InvoiceId, ct: cancellationToken);
        if (entity == null)
            return null;

        if (entity.SyncStatusId >= (short)SyncStatus.Processed)
            throw new InvoizRException($"Change processing status for '{entity.Id}' ({entity.InvoiceNumber}) invoice can't be completed.");

        entity.SyncStatusId = (short)SyncStatus.Requested;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new();
    }
}
