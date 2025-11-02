using InvoizR.Domain.Entities;

namespace InvoizR.Application.Common.Contracts;

public interface IFallbackExportStrategy
{
    Task<byte[]> ExportAsync(Fallback fallback, string path = "", CancellationToken ct = default);
    string ContentType { get; }
    string FileExtension { get; }
}
