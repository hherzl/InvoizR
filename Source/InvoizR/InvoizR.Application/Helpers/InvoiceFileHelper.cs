using InvoizR.Domain.Entities;

namespace InvoizR.Application.Helpers;

public static class InvoiceFileHelper
{
    private const string ApplicationJson = "application/json";
    private const string ApplicationPdf = "application/pdf";

    public static async Task<InvoiceFile> CreateJsonAsync(Invoice invoice, string path, CancellationToken cancellationToken = default)
        => InvoiceFile.Create(invoice, await File.ReadAllBytesAsync(path, cancellationToken), ApplicationJson, "JSON");

    public static async Task<InvoiceFile> CreatePdfAsync(Invoice invoice, string path, CancellationToken cancellationToken = default)
        => InvoiceFile.Create(invoice, await File.ReadAllBytesAsync(path, cancellationToken), ApplicationPdf, "PDF");
}
