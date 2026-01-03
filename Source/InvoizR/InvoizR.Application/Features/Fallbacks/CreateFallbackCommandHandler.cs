using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Fallback;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using MediatR;

namespace InvoizR.Application.Features.Fallbacks;

public sealed class CreateFallbackCommandHandler(IInvoizRDbContext dbContext) : IRequestHandler<CreateFallbackCommand, CreatedResponse<short?>>
{
    public async Task<CreatedResponse<short?>> Handle(CreateFallbackCommand request, CancellationToken ct)
    {
        var existingEntity = await dbContext.GetFallbackByCompanyAndNameAsync(request.CompanyId, request.Name, ct: ct);
        if (existingEntity != null)
            return new(existingEntity.Id);

        var fallback = new Fallback
        {
            CompanyId = request.CompanyId,
            Name = request.Name,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            Enable = true,
            FallbackGuid = request.Contingencia.Identificacion.CodigoGeneracion,
            SyncStatusId = (short)SyncStatus.Created,
            Payload = request.Contingencia.ToJson(),
            RetryIn = 0,
            SyncAttempts = 0
        };

        dbContext.Fallback.Add(fallback);

        await dbContext.SaveChangesAsync(ct);

        return new(fallback.Id);
    }
}
