namespace InvoizR.Infrastructure.Reports;

public static class ReportsPathHelper
{
    const string Assets = "Assets";
    const string Css = "css";

    public static string GetDteCssPath()
        => Path.Combine(AppContext.BaseDirectory, Assets, Css, "dte.css");

    public static string GetFallbackCssPath()
        => Path.Combine(AppContext.BaseDirectory, Assets, Css, "fallback.css");
}
