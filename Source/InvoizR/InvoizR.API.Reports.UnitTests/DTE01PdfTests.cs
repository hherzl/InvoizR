using DinkToPdf;
using InvoizR.Application.Helpers;
using InvoizR.Application.Reports.Templates;
using InvoizR.Infrastructure.Reports;

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
        var invoice = await _dbContext.GetInvoiceAsync(1, includes: true);
        var pdfPath = _processingSettings.GetDtePdfPath(invoice.ControlNumber);
        var pdf = new HtmlToPdfDocument
        {
            GlobalSettings = DinkToPdfHelper.CreateDteGlobalSettings(pdfPath),
            Objects =
            {
                DinkToPdfHelper.CreateDteObjSettings(new Dte01Templatev1(new Dte01TemplateFactory(_qrCodeGenerator).Create(invoice)).ToString())
            }
        };

        var bytes = _converter.Convert(pdf);

        Assert.True(bytes.Length > 0);
    }

    [Fact]
    public async Task GenerateDTE03Pdf()
    {
        var invoice = await _dbContext.GetInvoiceAsync(10, includes: true);
        var pdfPath = _processingSettings.GetDtePdfPath(invoice.ControlNumber);
        var pdf = new HtmlToPdfDocument
        {
            GlobalSettings = DinkToPdfHelper.CreateDteGlobalSettings(pdfPath),
            Objects =
            {
                DinkToPdfHelper.CreateDteObjSettings(new Dte03Templatev1(new Dte03TemplateFactory(_qrCodeGenerator).Create(invoice)).ToString())
            }
        };

        var bytes = _converter.Convert(pdf);

        await File.WriteAllBytesAsync(pdfPath, bytes);

        Assert.True(bytes.Length > 0);
    }
}
