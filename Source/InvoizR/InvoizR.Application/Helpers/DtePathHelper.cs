using InvoizR.Application.Common;

namespace InvoizR.Application.Helpers;

public static class DtePathHelper
{
    public static string GetDteDirectory(this ProcessingSettings settings, string controlNumber)
        => Path.Combine(settings.OutputPath, settings.DtePath, controlNumber);

    public static string GetLogsPath(this ProcessingSettings settings, string controlNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, controlNumber);

    public static string GetFirmaRequestJsonPath(this ProcessingSettings settings, string controlNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, controlNumber, $"{controlNumber}.firma.request.json");

    public static string GetFirmaResponseJsonPath(this ProcessingSettings settings, string controlNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, controlNumber, $"{controlNumber}.firma.response.json");

    public static string GetRecepcionRequestJsonPath(this ProcessingSettings settings, string controlNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, controlNumber, $"{controlNumber}.recepcion.request.json");

    public static string GetRecepcionResponseJsonPath(this ProcessingSettings settings, string controlNumber)
        => Path.Combine(settings.OutputPath, settings.LogsPath, controlNumber, $"{controlNumber}.recepcion.response.json");

    public static string GetDteJsonPath(this ProcessingSettings settings, string controlNumber)
        => Path.Combine(settings.OutputPath, settings.DtePath, $"{controlNumber}.json");

    public static string GetDtePdfPath(this ProcessingSettings settings, string controlNumber)
        => Path.Combine(settings.OutputPath, settings.DtePath, $"{controlNumber}.pdf");

    public static string GetDteNotificationPath(this ProcessingSettings settings, string controlNumber)
        => Path.Combine(settings.OutputPath, settings.NotificationsPath, $"{controlNumber}.html");
}
