using DinkToPdf;
using DinkToPdf.Contracts;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Reports.Templates;
using InvoizR.Domain.Entities;
using InvoizR.Infrastructure.Reports;
using InvoizR.SharedKernel.Mh.FeCcf;
using InvoizR.SharedKernel.Mh.FeFc;
using InvoizR.SharedKernel.Mh.FeFse;
using InvoizR.SharedKernel.Mh.FeNc;
using InvoizR.SharedKernel.Mh.FeNd;
using InvoizR.SharedKernel.Mh.FeNr;
using Microsoft.Extensions.Logging;

namespace InvoizR.Infrastructure.FileExport;

public class PdfInvoiceExportStrategy : IInvoiceExportStrategy
{
    private readonly ILogger _logger;
    private readonly IConverter _converter;
    private readonly Dte01TemplateFactory _dte01TemplateFactory;
    private readonly Dte03TemplateFactory _dte03TemplateFactory;
    private readonly Dte04TemplateFactory _dte04TemplateFactory;
    private readonly Dte05TemplateFactory _dte05TemplateFactory;
    private readonly Dte06TemplateFactory _dte06TemplateFactory;
    private readonly Dte14TemplateFactory _dte14TemplateFactory;

    public PdfInvoiceExportStrategy
    (
        ILogger<PdfInvoiceExportStrategy> logger,
        IConverter converter,
        Dte01TemplateFactory dte01TemplateFactory,
        Dte03TemplateFactory dte03TemplateFactory,
        Dte04TemplateFactory dte04TemplateFactory,
        Dte05TemplateFactory dte05TemplateFactory,
        Dte06TemplateFactory dte06TemplateFactory,
        Dte14TemplateFactory dte14TemplateFactory
    )
    {
        _logger = logger;
        _converter = converter;
        _dte01TemplateFactory = dte01TemplateFactory;
        _dte03TemplateFactory = dte03TemplateFactory;
        _dte04TemplateFactory = dte04TemplateFactory;
        _dte05TemplateFactory = dte05TemplateFactory;
        _dte06TemplateFactory = dte06TemplateFactory;
        _dte14TemplateFactory = dte14TemplateFactory;
    }

    public async Task<byte[]> ExportAsync(Invoice invoice, string path = "", CancellationToken cancellationToken = default)
    {
        ObjectSettings objSettings = null;

        if (invoice.InvoiceTypeId == FeFcv1.TypeId)
            objSettings = DinkToPdfHelper.CreateDteObjSettings(new Dte01Templatev1(_dte01TemplateFactory.Create(invoice)).ToString(), ReportsPathHelper.GetInvoiceCssPath());
        else if (invoice.InvoiceTypeId == FeCcfv3.TypeId)
            objSettings = DinkToPdfHelper.CreateDteObjSettings(new Dte03Templatev1(_dte03TemplateFactory.Create(invoice)).ToString(), ReportsPathHelper.GetInvoiceCssPath());
        else if (invoice.InvoiceTypeId == FeNrv3.TypeId)
            objSettings = DinkToPdfHelper.CreateDteObjSettings(new Dte04Templatev1(_dte04TemplateFactory.Create(invoice)).ToString(), ReportsPathHelper.GetInvoiceCssPath());
        else if (invoice.InvoiceTypeId == FeNcv3.TypeId)
            objSettings = DinkToPdfHelper.CreateDteObjSettings(new Dte05Templatev1(_dte05TemplateFactory.Create(invoice)).ToString(), ReportsPathHelper.GetInvoiceCssPath());
        else if (invoice.InvoiceTypeId == FeNdv3.TypeId)
            objSettings = DinkToPdfHelper.CreateDteObjSettings(new Dte06Templatev1(_dte06TemplateFactory.Create(invoice)).ToString(), ReportsPathHelper.GetInvoiceCssPath());
        else if (invoice.InvoiceTypeId == FeFsev1.TypeId)
            objSettings = DinkToPdfHelper.CreateDteObjSettings(new Dte14Templatev1(_dte14TemplateFactory.Create(invoice)).ToString(), ReportsPathHelper.GetInvoiceCssPath());

        var pdfDocument = new HtmlToPdfDocument
        {
            GlobalSettings = DinkToPdfHelper.CreateDteGlobalSettings(),
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
