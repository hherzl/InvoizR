using System.Text;
using InvoizR.Application.Common.Contracts;
using InvoizR.Domain.Entities;
using InvoizR.SharedKernel;
using Microsoft.Extensions.Logging;

namespace InvoizR.Infrastructure.FileExport;

public class JsonInvoiceExportStrategy : IInvoiceExportStrategy
{
    private readonly ILogger _logger;

    public JsonInvoiceExportStrategy(ILogger<JsonInvoiceExportStrategy> logger)
    {
        _logger = logger;
    }

    public async Task<byte[]> ExportAsync(Invoice invoice, string path = "", CancellationToken ct = default)
    {
        var array = Encoding.UTF8.GetBytes(invoice.Payload);

        if (!string.IsNullOrEmpty(path))
        {
            _logger.LogInformation($"Creating JSON file for invoice '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}', path: '{path}'...");
            await File.WriteAllBytesAsync(path, array, ct);
        }

        return array;
    }

    public string ContentType => Tokens.ApplicationJson;
    public string FileExtension => Tokens.Json;
}
