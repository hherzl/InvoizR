using InvoizR.Application.Common;

namespace InvoizR.Application;

public static class PathExtensions
{
    public static string GetLogsPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, auditNumber);

    public static string GetFirmaRequestJsonPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, auditNumber, $"{auditNumber}.firma.request.json");

    public static string GetFirmaResponseJsonPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, auditNumber, $"{auditNumber}.firma.response.json");

    public static string GetRecepcionRequestJsonPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, auditNumber, $"{auditNumber}.recepcion.request.json");

    public static string GetRecepcionResponseJsonPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, auditNumber, $"{auditNumber}.recepcion.response.json");

    public static string GetInvoicePath(this ProcessingSettings settings, string auditNumber, string extension)
        => Path.Combine(settings.OutputPath, settings.InvoicesPath, $"{auditNumber}.{extension}");

    public static string GetInvoicePdfPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.InvoicesPath, $"{auditNumber}.pdf");

    public static string GetInvoiceNotificationPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.NotificationsPath, $"{auditNumber}.html");

    public static string GetInvoiceCancellationFirmaRequestJsonPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, auditNumber, $"{auditNumber}.cancellation.firma.request.json");

    public static string GetInvoiceCancellationFirmaResponseJsonPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, auditNumber, $"{auditNumber}.cancellation.firma.response.json");

    public static string GetInvoiceCancellationRequestJsonPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, auditNumber, $"{auditNumber}.cancellation.request.json");

    public static string GetInvoiceCancellationResponseJsonPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, auditNumber, $"{auditNumber}.cancellation.response.json");

    public static string GetFallbackPath(this ProcessingSettings settings, string guid, string extension)
        => Path.Combine(settings.OutputPath, settings.FallbacksPath, $"{guid}.{extension}");
}
