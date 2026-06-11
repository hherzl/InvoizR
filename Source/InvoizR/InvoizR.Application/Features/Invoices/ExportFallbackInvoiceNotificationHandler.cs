using InvoizR.Application.Services;
using InvoizR.Domain.Notifications;
using MediatR;

namespace InvoizR.Application.Features.Invoices;

public sealed class ExportFallbackInvoiceNotificationHandler(FallbackInvoiceExporter invoiceExporter) : INotificationHandler<ExportFallbackInvoiceNotification>
{
    public async Task Handle(ExportFallbackInvoiceNotification notification, CancellationToken ct)
    {
        await invoiceExporter.ExportAsync(notification.Id, ct);
    }
}
