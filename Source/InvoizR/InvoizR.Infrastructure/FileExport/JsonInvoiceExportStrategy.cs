using System.Text;
using InvoizR.Application.Common.Contracts;
using InvoizR.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace InvoizR.Infrastructure.FileExport;

public class JsonInvoiceExportStrategy : IInvoiceExportStrategy
{
    private readonly ILogger _logger;

    public JsonInvoiceExportStrategy(ILogger<JsonInvoiceExportStrategy> logger)
    {
        _logger = logger;
    }

    public async Task<byte[]> ExportAsync(Invoice invoice, string path = "", CancellationToken cancellationToken = default)
    {
        var array = Encoding.UTF8.GetBytes(invoice.Serialization);

        if (!string.IsNullOrEmpty(path))
        {
            _logger.LogInformation($"Creating JSON file for invoice '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}', path: '{path}'...");
            await File.WriteAllBytesAsync(path, array, cancellationToken);
        }

        return array;
    }

    public string ContentType => "application/json";
    public string FileExtension => "json";
}
