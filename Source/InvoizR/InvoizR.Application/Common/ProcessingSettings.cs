namespace InvoizR.Application.Common;

public record ProcessingSettings
{
    public string OutputPath { get; set; }
    public string LogsPath { get; set; }
    public string DtePath { get; set; }
    public string NotificationsPath { get; set; }
}
