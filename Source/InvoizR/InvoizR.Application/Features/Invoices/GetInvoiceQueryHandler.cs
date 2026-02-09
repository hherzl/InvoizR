using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Invoices;
using InvoizR.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Invoices;

public sealed class GetInvoiceQueryHandler(IInvoizRDbContext dbContext) : IRequestHandler<GetInvoiceQuery, SingleResponse<InvoiceDetailsModel>>
{
    public async Task<SingleResponse<InvoiceDetailsModel>> Handle(GetInvoiceQuery request, CancellationToken ct = default)
    {
        var entity = await dbContext.GetInvoiceAsync(request.Id, ct: ct);
        if (entity == null)
            return null;

        var syncStatuses = await dbContext.VInvoiceSyncStatuses.ToListAsync(ct);
        var syncStatusLogs = await dbContext.GetInvoiceSyncStatusLogs(entity.Id).ToListAsync(ct);
        var syncLogs = await dbContext.GetInvoiceSyncLogs(entity.Id).ToListAsync(ct);
        var files = await dbContext.GetInvoiceFilesBy(entity.Id).ToListAsync(ct);
        var notifications = await dbContext.GetInvoiceNotifications(entity.Id).ToListAsync(ct);

        var model = new InvoiceDetailsModel
        {
            Id = entity.Id,
            FallbackId = entity.FallbackId,
            PosId = entity.PosId,
            CustomerId = entity.CustomerId,
            CustomerWtId = entity.CustomerWtId,
            CustomerName = entity.CustomerName,
            CustomerCountryId = entity.CustomerCountryId,
            CustomerCountryLevelId = entity.CustomerCountryLevelId,
            CustomerAddress = entity.CustomerAddress,
            CustomerPhone = entity.CustomerPhone,
            CustomerEmail = entity.CustomerEmail,
            CustomerLastUpdated = entity.CustomerLastUpdated,
            InvoiceTypeId = entity.InvoiceTypeId,
            InvoiceNumber = entity.InvoiceNumber,
            InvoiceDate = entity.InvoiceDate,
            InvoiceTotal = entity.InvoiceTotal,
            Lines = entity.Lines,
            SchemaType = entity.SchemaType,
            SchemaVersion = entity.SchemaVersion,
            InvoiceGuid = entity.InvoiceGuid,
            AuditNumber = entity.AuditNumber,
            Payload = entity.Payload,
            SyncStatusId = entity.SyncStatusId,
            SyncStatus = syncStatuses.First(item => item.Id == entity.SyncStatusId).Desc,
            RetryIn = entity.RetryIn,
            SyncAttempts = entity.SyncAttempts,
            EmitDateTime = entity.EmitDateTime,
            ReceiptStamp = entity.ReceiptStamp,
            ExternalUrl = entity.ExternalUrl,
            Notes = entity.Notes,
            CreatedAt = entity.CreatedAt,
            Processed = entity.SyncStatusId >= (short)SyncStatus.Processed,
            SyncStatusLogs = syncStatusLogs,
            SyncLogs = syncLogs,
            Files = files,
            Notifications = notifications
        };

        return new(model);
    }
}
