using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.SharedKernel.Mh;

namespace InvoizR.Application.Services.Models;

public record CreateDte01Request : ICreateDteRequest<FeFcv1>
{
    public static ICreateDteRequest<FeFcv1> Create(MhSettings mhSettings, ProcessingSettings processingSettings, string jwt, long? invoiceId, string payload)
    {
        var dte = FeFcv1.Deserialize(payload);

        return new CreateDte01Request
        {
            MhSettings = mhSettings,
            ProcessingSettings = processingSettings,
            Jwt = jwt,
            InvoiceId = invoiceId,
            Dte = dte
        };
    }

    public CreateDte01Request()
    {
    }

    public MhSettings MhSettings { get; set; }
    public ProcessingSettings ProcessingSettings { get; set; }
    public string Jwt { get; set; }
    public long? InvoiceId { get; set; }
    public FeFcv1 Dte { get; set; }
}
