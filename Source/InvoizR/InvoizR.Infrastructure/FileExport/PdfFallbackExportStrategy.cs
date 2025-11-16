using DinkToPdf;
using DinkToPdf.Contracts;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Reports.Templates;
using InvoizR.Domain.Entities;
using InvoizR.Infrastructure.Reports;
using InvoizR.SharedKernel;
using Microsoft.Extensions.Logging;

namespace InvoizR.Infrastructure.FileExport;

public class PdfFallbackExportStrategy : IFallbackExportStrategy
{
    private readonly ILogger _logger;
    private readonly IConverter _converter;
    private readonly FallbackTemplateFactory _fallbackTemplateFactory;

    public PdfFallbackExportStrategy(ILogger<PdfFallbackExportStrategy> logger, IConverter converter, FallbackTemplateFactory fallbackTemplateFactory)
    {
        _logger = logger;
        _converter = converter;
        _fallbackTemplateFactory = fallbackTemplateFactory;
    }

    public async Task<byte[]> ExportAsync(Fallback fallback, string path = "", CancellationToken ct = default)
    {
        var objSettings = DinkToPdfHelper.CreateDteObjSettings(new FallbackTemplatev1(_fallbackTemplateFactory.Create(fallback)).ToString(), ReportsPathHelper.GetFallbackCssPath());

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
            _logger.LogInformation($"Creating PDF file for fallback '{fallback.Id}-{fallback.Name}', path: '{path}'...");
            await File.WriteAllBytesAsync(path, array, ct);
        }

        return array;
    }

    public string ContentType => Tokens.ApplicationPdf;
    public string FileExtension => Tokens.Pdf;
}
