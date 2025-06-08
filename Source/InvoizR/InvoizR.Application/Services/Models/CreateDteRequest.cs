using InvoizR.Application.Common;

namespace InvoizR.Application.Services.Models;

public record CreateDteRequest
{
    public CreateDteRequest()
    {
    }

    public CreateDteRequest(MhSettings mhSettings, ProcessingSettings processingSettings, string jwt, long? invoiceId)
    {
        MhSettings = mhSettings;
        ProcessingSettings = processingSettings;
        Jwt = jwt;
        InvoiceId = invoiceId;
    }

    public MhSettings MhSettings { get; set; }
    public ProcessingSettings ProcessingSettings { get; set; }
    public string Jwt { get; set; }
    public long? InvoiceId { get; set; }
}
