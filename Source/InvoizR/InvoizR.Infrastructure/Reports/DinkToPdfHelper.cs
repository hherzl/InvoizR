﻿using DinkToPdf;

namespace InvoizR.Infrastructure.Reports;

public class DinkToPdfHelper
{
    public static GlobalSettings CreateDteGlobalSettings(string fileName = "", string documentTitle = "Documento Tributario Electrónico")
    {
        var obj = new GlobalSettings
        {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4,
            Margins = new MarginSettings
            {
                Top = 10,
                Right = 5,
                Left = 5
            },
            DocumentTitle = documentTitle
        };

        if (!string.IsNullOrEmpty(fileName))
            obj.Out = fileName;

        return obj;
    }

    public static ObjectSettings CreateDteObjSettings(string htmlContent)
       => new()
       {
           PagesCount = true,
           HtmlContent = htmlContent,
           WebSettings =
           {
                DefaultEncoding = "utf-8",
                UserStyleSheet = ReportsPathHelper.GetDteCssPath()
           },
           HeaderSettings =
           {
                FontName = "Arial",
                FontSize = 8,
                Right = "Página [page] de [toPage]",
                Line = true
           },
           FooterSettings =
           {
               FontName = "Arial",
                FontSize = 10,
                Line = true,
                Center = "Capsule Corp."
           }
       };
}
