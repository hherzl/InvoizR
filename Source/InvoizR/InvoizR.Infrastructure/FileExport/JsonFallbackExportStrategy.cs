using System.Text;
using InvoizR.Application.Common.Contracts;
using InvoizR.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace InvoizR.Infrastructure.FileExport;

public class JsonFallbackExportStrategy : IFallbackExportStrategy
{
    private readonly ILogger _logger;

    public JsonFallbackExportStrategy(ILogger<JsonFallbackExportStrategy> logger)
    {
        _logger = logger;
    }

    public async Task<byte[]> ExportAsync(Fallback fallback, string path = "", CancellationToken ct = default)
    {
        var array = Encoding.UTF8.GetBytes(fallback.Payload);

        if (!string.IsNullOrEmpty(path))
        {
            _logger.LogInformation($"Creating JSON file for fallback '{fallback.Id}-{fallback.Name}', path: '{path}'...");
            await File.WriteAllBytesAsync(path, array, ct);
        }

        return array;
    }

    public string ContentType => "application/json";
    public string FileExtension => "json";
}
