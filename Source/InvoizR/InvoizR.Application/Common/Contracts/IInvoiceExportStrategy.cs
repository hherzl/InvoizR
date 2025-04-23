using InvoizR.Domain.Entities;

namespace InvoizR.Application.Common.Contracts;

public interface IInvoiceExportStrategy
{
    Task<byte[]> ExportAsync(Invoice invoice, string path = "", CancellationToken ct = default);
    string ContentType { get; }
    string FileExtension { get; }
}
