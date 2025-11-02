using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Fallback;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Fallbacks.Queries;

public sealed class GetFallbackQueryHandler(IInvoizRDbContext dbContext) : IRequestHandler<GetFallbackQuery, SingleResponse<FallbackDetailsModel>>
{
    public async Task<SingleResponse<FallbackDetailsModel>> Handle(GetFallbackQuery request, CancellationToken st)
    {
        var entity = await dbContext.GetFallbackAsync(request.Id, includes: true, ct: st);
        if (entity == null)
            return null;

        var processingStatuses = await dbContext.VInvoiceProcessingStatus.ToListAsync(st);
        var invoices = await dbContext.GetInvoicesByFallback(entity.Id).ToListAsync(st);
        var processingLogs = await dbContext.GetFallbackProcessingLogs(entity.Id).ToListAsync(st);
        var files = await dbContext.GetFallbackFiles(entity.Id).ToListAsync(st);

        var model = new FallbackDetailsModel
        {
            Id = entity.Id,
            CompanyId = entity.CompanyId,
            Company = entity.Company.Name,
            Name = entity.Name,
            StartDateTime = entity.StartDateTime,
            EndDateTime = entity.EndDateTime,
            Enable = entity.Enable,
            FallbackGuid = entity.FallbackGuid,
            SyncStatusId = entity.SyncStatusId,
            SyncStatus = processingStatuses.FirstOrDefault(item => item.Id == entity.SyncStatusId)?.Desc,
            Payload = entity.Payload,
            RetryIn = entity.RetryIn,
            SyncAttempts = entity.SyncAttempts,
            EmitDateTime = entity.EmitDateTime,
            ReceiptStamp = entity.ReceiptStamp,
            Invoices = invoices,
            ProcessingLogs = processingLogs,
            Files = files
        };

        return new(model);
    }
}
