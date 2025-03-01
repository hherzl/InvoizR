using DinkToPdf;
using InvoizR.API.Reports.Helpers;
using InvoizR.API.Reports.Templates.Pdf;
using InvoizR.Application.Helpers;

namespace InvoizR.API.Reports.UnitTests;

public class DTE01PdfTests : ReportsTest
{
    public DTE01PdfTests()
        : base()
    {
    }

    [Fact]
    public async Task GenerateDTE01Pdf()
    {
        var invoice = await _dbContext.GetInvoiceAsync(10, includes: true);
        var model = Dte01TemplateFactory.Create(invoice);

        var pdfPath = _processingSettings.GetDtePdfPath(invoice.ControlNumber);
        var pdf = new HtmlToPdfDocument
        {
            GlobalSettings = DinkToPdfHelper.CreateDteGlobalSettings(pdfPath),
            Objects =
            {
                DinkToPdfHelper.CreateDteObjSettings(new DteTemplatev1(model).ToString())
            }
        };

        _converter.Convert(pdf);
    }
}
