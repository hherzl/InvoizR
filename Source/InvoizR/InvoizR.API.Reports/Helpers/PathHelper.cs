namespace InvoizR.API.Reports.Helpers;

public static class ReportsPathHelper
{
    const string Assets = "assets";

    const string Css = "css";
    const string Img = "img";

    public static string GetAssetsPath()
        => Path.Combine(Directory.GetCurrentDirectory(), Assets);

    public static string GetDteCssPath()
        => Path.Combine(Directory.GetCurrentDirectory(), Assets, Css, "dte.css");
}
