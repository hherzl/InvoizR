using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.SharedKernel.Mh.FeCcf;

namespace InvoizR.Application.Services.Models;

public class CreateDte03Request : ICreateDteRequest<FeCcfv3>
{
    public static ICreateDteRequest<FeCcfv3> Create(MhSettings mhSettings, ProcessingSettings processingSettings, string jwt, long? invoiceId, string payload)
    {
        var dte = FeCcfv3.Deserialize(payload);

        return new CreateDte03Request
        {
            MhSettings = mhSettings,
            ProcessingSettings = processingSettings,
            Jwt = jwt,
            InvoiceId = invoiceId,
            Dte = dte
        };
    }

    public CreateDte03Request()
    {
    }

    public MhSettings MhSettings { get; set; }
    public ProcessingSettings ProcessingSettings { get; set; }
    public string Jwt { get; set; }
    public long? InvoiceId { get; set; }
    public FeCcfv3 Dte { get; set; }
}
