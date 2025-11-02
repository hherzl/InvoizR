using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Fallback;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using MediatR;

namespace InvoizR.Application.Features.Fallbacks.Commands;

public sealed class CreateFallbackCommandHandler : IRequestHandler<CreateFallbackCommand, CreatedResponse<short?>>
{
    private readonly IInvoizRDbContext _dbContext;

    public CreateFallbackCommandHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreatedResponse<short?>> Handle(CreateFallbackCommand request, CancellationToken cancellationToken)
    {
        var existingEntity = await _dbContext.GetFallbackByCompanyAndNameAsync(request.CompanyId, request.Name, ct: cancellationToken);
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
            SyncStatusId = (short)InvoiceProcessingStatus.Created,
            Payload = request.Contingencia.ToJson(),
            RetryIn = 0,
            SyncAttempts = 0
        };

        _dbContext.Fallback.Add(fallback);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(fallback.Id);
    }
}
