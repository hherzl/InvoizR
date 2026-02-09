using InvoizR.Application.Common.Contracts;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Features.Invoices;

public sealed class CancelInvoiceNotificationHandler(ILogger<CancelInvoiceNotificationHandler> logger, IInvoizRDbContext dbContext)
    : INotificationHandler<CancelInvoiceNotification>
{
    public async Task Handle(CancelInvoiceNotification notification, CancellationToken ct)
    {
        logger.LogInformation($"Cancelling '{notification.Id}' invoice");

        var invoice = await dbContext.GetInvoiceAsync(notification.Id, true, true, ct);

        invoice.SyncStatusId = (short)SyncStatus.Cancelled;
        invoice.CancellationPayload = notification.Payload;
        invoice.CancellationDateTime = DateTime.Now;

        dbContext.InvoiceSyncStatusLogs.Add(new(invoice.Id, invoice.SyncStatusId));

        await dbContext.SaveChangesAsync(ct);
    }
}
