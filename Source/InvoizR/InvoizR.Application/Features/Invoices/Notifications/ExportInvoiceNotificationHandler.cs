using InvoizR.Application.Services;
using InvoizR.Domain.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Features.Invoices.Notifications;

public sealed class ExportInvoiceNotificationHandler : INotificationHandler<ExportInvoiceNotification>
{
    private readonly ILogger _logger;
    private readonly DteExporterService _dteExporterService;

    public ExportInvoiceNotificationHandler(ILogger<ExportInvoiceNotificationHandler> logger, DteExporterService dteExportService)
    {
        _logger = logger;
        _dteExporterService = dteExportService;
    }

    public async Task Handle(ExportInvoiceNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Exporting '{notification.Id}' invoice, type: '{notification.InvoiceTypeId}'");

        await _dteExporterService.ExportAsync(notification.Id, cancellationToken);
    }
}
