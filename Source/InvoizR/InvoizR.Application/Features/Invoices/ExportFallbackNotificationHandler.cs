using InvoizR.Application.Services;
using InvoizR.Domain.Notifications;
using MediatR;

namespace InvoizR.Application.Features.Invoices;

public sealed class ExportFallbackNotificationHandler(FallbackExporter fallbackExporter) : INotificationHandler<ExportFallbackNotification>
{
    public async Task Handle(ExportFallbackNotification notification, CancellationToken ct)
    {
        await fallbackExporter.ExportAsync(notification.Id, ct);
    }
}
