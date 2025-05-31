using DinkToPdf;
using DinkToPdf.Contracts;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Reports.Templates;
using InvoizR.Domain.Entities;
using InvoizR.Infrastructure.Reports;
using InvoizR.SharedKernel.Mh.FeCcf;
using InvoizR.SharedKernel.Mh.FeFc;
using Microsoft.Extensions.Logging;

namespace InvoizR.Infrastructure.FileExport;

public class PdfInvoiceExportStrategy : IInvoiceExportStrategy
{
    private readonly ILogger _logger;
    private readonly IConverter _converter;
    private readonly Dte01TemplateFactory _dte01TemplateFactory;
    private readonly Dte03TemplateFactory _dte03TemplateFactory;

    public PdfInvoiceExportStrategy(ILogger<PdfInvoiceExportStrategy> logger, IConverter converter, Dte01TemplateFactory dte01TemplateFactory, Dte03TemplateFactory dte03TemplateFactory)
    {
        _logger = logger;
        _converter = converter;
        _dte01TemplateFactory = dte01TemplateFactory;
        _dte03TemplateFactory = dte03TemplateFactory;
    }

    public async Task<byte[]> ExportAsync(Invoice invoice, string path = "", CancellationToken cancellationToken = default)
    {
        ObjectSettings objSettings = null;

        if (invoice.InvoiceTypeId == FeFcv1.TypeId)
            objSettings = DinkToPdfHelper.CreateDteObjSettings(new Dte01Templatev1(_dte01TemplateFactory.Create(invoice)).ToString());
        else if (invoice.InvoiceTypeId == FeCcfv3.TypeId)
            objSettings = DinkToPdfHelper.CreateDteObjSettings(new Dte03Templatev1(_dte03TemplateFactory.Create(invoice)).ToString());

        var pdfDocument = new HtmlToPdfDocument
        {
            GlobalSettings = DinkToPdfHelper.CreateDteGlobalSettings(fileName: path),
            Objects =
            {
                objSettings
            }
        };

        var array = _converter.Convert(pdfDocument);

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
