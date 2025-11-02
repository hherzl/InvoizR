using InvoizR.Application.Services;
using InvoizR.Domain.Notifications;
using MediatR;

namespace InvoizR.Application.Features.Invoices.Notifications;

public sealed class ExportInvoiceNotificationHandler(InvoiceExporter invoiceExporter)
    : INotificationHandler<ExportInvoiceNotification>
{
    public async Task Handle(ExportInvoiceNotification notification, CancellationToken st)
    {
        await invoiceExporter.ExportAsync(notification.Id, st);
    }
}
