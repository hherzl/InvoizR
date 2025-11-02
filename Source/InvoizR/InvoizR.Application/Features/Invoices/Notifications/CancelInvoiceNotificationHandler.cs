using InvoizR.Application.Common.Contracts;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Features.Invoices.Notifications;

public sealed class CancelInvoiceNotificationHandler(ILogger<CancelInvoiceNotificationHandler> logger, IInvoizRDbContext dbContext)
    : INotificationHandler<CancelInvoiceNotification>
{
    public async Task Handle(CancelInvoiceNotification notification, CancellationToken st)
    {
        logger.LogInformation($"Cancelling '{notification.Id}' invoice");

        var invoice = await dbContext.GetInvoiceAsync(notification.Id, true, true, st);

        invoice.ProcessingStatusId = (short)InvoiceProcessingStatus.Cancelled;
        invoice.CancellationPayload = notification.Payload;
        invoice.CancellationDateTime = DateTime.Now;

        dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));

        await dbContext.SaveChangesAsync(st);
    }
}
