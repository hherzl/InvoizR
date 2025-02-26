using InvoizR.Application.Common;
using InvoizR.SharedKernel.Mh;

namespace InvoizR.Application.Services.Models;

public record CreateDte01Request
{
    public CreateDte01Request()
    {
    }

    public CreateDte01Request(MhSettings mhSettings, ProcessingSettings processingSettings, string jwt, long? invoiceId)
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

    public FeFcv1 Dte { get; }
}
