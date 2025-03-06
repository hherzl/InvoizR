using DinkToPdf;
using DinkToPdf.Contracts;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Reports.Templates;
using InvoizR.Domain.Entities;
using InvoizR.Infrastructure.Reports;
using Microsoft.Extensions.Logging;

namespace InvoizR.Infrastructure.FileExport;

public class PdfInvoiceExportStrategy : IInvoiceExportStrategy
{
    private readonly ILogger _logger;
    private readonly Dte01TemplateFactory _templateFactory;
    private readonly IConverter _converter;

    public PdfInvoiceExportStrategy(ILogger<PdfInvoiceExportStrategy> logger, Dte01TemplateFactory templateFactory)
    {
        _logger = logger;
        _templateFactory = templateFactory;
        _converter = new SynchronizedConverter(new PdfTools());
    }

    public async Task<byte[]> ExportAsync(Invoice invoice, string path = "", CancellationToken cancellationToken = default)
    {
        var model = _templateFactory.Create(invoice);

        var pdf = new HtmlToPdfDocument
        {
            GlobalSettings = DinkToPdfHelper.CreateDteGlobalSettings(),
            Objects =
            {
                DinkToPdfHelper.CreateDteObjSettings(new DteTemplatev1(model).ToString())
            }
        };

        var array = _converter.Convert(pdf);

        if (!string.IsNullOrEmpty(path))
        {
            _logger.LogInformation($"Creating PDF file for invoice '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}', path: '{path}'...");
            await File.WriteAllBytesAsync(path, array, cancellationToken);
        }

        return array;
    }

    public string ContentType => "application/pdf";
    public string FileExtension => "pdf";
}
