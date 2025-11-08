namespace InvoizR.Infrastructure.Reports;

public static class ReportsPathHelper
{
    const string Assets = "Assets";
    const string Css = "css";

    public static string GetInvoiceCssPath()
        => Path.Combine(AppContext.BaseDirectory, Assets, Css, "invoice.css");

    public static string GetFallbackCssPath()
        => Path.Combine(AppContext.BaseDirectory, Assets, Css, "fallback.css");
}
