using InvoizR.Application.Services;
using InvoizR.Domain.Notifications;
using MediatR;

namespace InvoizR.Application.Features.Invoices.Notifications;

public sealed class ExportFallbackInvoiceNotificationHandler(FallbackInvoiceExporter invoiceExporter)
    : INotificationHandler<ExportFallbackInvoiceNotification>
{
    public async Task Handle(ExportFallbackInvoiceNotification notification, CancellationToken st)
    {
        await invoiceExporter.ExportAsync(notification.Id, st);
    }
}
