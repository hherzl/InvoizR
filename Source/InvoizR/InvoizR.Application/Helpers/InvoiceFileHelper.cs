using InvoizR.Domain.Entities;

namespace InvoizR.Application.Helpers;

public static class InvoiceFileHelper
{
    private const string ApplicationJson = "application/json";
    private const string ApplicationPdf = "application/pdf";

    public static InvoiceFile CreateJson(Invoice invoice, byte[] data)
        => InvoiceFile.Create(invoice, data, ApplicationJson, "JSON");

    public static InvoiceFile CreatePdf(Invoice invoice, byte[] data)
        => InvoiceFile.Create(invoice, data, ApplicationPdf, "PDF");
}
