using InvoizR.Application.Common;

namespace InvoizR.Application.Helpers;

public static class DtePathHelper
{
    public static string GetDteDirectory(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.DtePath, auditNumber);

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

    public static string GetDtePath(this ProcessingSettings settings, string auditNumber, string extension)
        => Path.Combine(settings.OutputPath, settings.DtePath, $"{auditNumber}.{extension}");

    public static string GetDteJsonPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.DtePath, $"{auditNumber}.json");

    public static string GetDtePdfPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.DtePath, $"{auditNumber}.pdf");

    public static string GetDteNotificationPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.NotificationsPath, $"{auditNumber}.html");

    public static string GetDteCancellationJsonPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.DtePath, $"{auditNumber}.cancellation.json");

    public static string GetDteCancellationFirmaRequestJsonPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, auditNumber, $"{auditNumber}.cancellation.firma.request.json");

    public static string GetDteCancellationFirmaResponseJsonPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, auditNumber, $"{auditNumber}.cancellation.firma.response.json");

    public static string GetDteCancellationRequestJsonPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, auditNumber, $"{auditNumber}.cancellation.request.json");

    public static string GetDteCancellationResponseJsonPath(this ProcessingSettings settings, string auditNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, auditNumber, $"{auditNumber}.cancellation.response.json");
}
