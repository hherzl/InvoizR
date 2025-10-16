using InvoizR.Application.Common.Contracts;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Features.Invoices.Notifications;

public sealed class CancelInvoiceNotificationHandler : INotificationHandler<CancelInvoiceNotification>
{
    private readonly ILogger _logger;
    private readonly IInvoizRDbContext _dbContext;

    public CancelInvoiceNotificationHandler(ILogger<CancelInvoiceNotificationHandler> logger, IInvoizRDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Handle(CancelInvoiceNotification notification, CancellationToken st)
    {
        _logger.LogInformation($"Cancelling '{notification.Id}' invoice");

        var invoice = await _dbContext.GetInvoiceAsync(notification.Id, true, true, st);

        invoice.ProcessingStatusId = (short)InvoiceProcessingStatus.Cancelled;
        invoice.CancellationPayload = notification.Payload;
        invoice.CancellationDateTime = DateTime.Now;

        _dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));

        await _dbContext.SaveChangesAsync(st);
    }
}
