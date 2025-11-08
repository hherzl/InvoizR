namespace InvoizR.Application.Common;

public record ProcessingSettings
{
    public string OutputPath { get; set; }
    public string LogsPath { get; set; }
    public string InvoicesPath { get; set; }
    public string NotificationsPath { get; set; }
    public string FallbacksPath { get; set; }
}
