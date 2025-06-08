using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.SharedKernel.Mh.FeCcf;

namespace InvoizR.Application.Services.Models;

public record CreateDte03Request : CreateDteRequest, ICreateDteRequest<FeCcfv3>
{
    public static ICreateDteRequest<FeCcfv3> Create(MhSettings mhSettings, ProcessingSettings processingSettings, string jwt, long? invoiceId, string payload)
    {
        var dte = FeCcfv3.Deserialize(payload);

        return new CreateDte03Request(mhSettings, processingSettings, jwt, invoiceId, dte);
    }

    public CreateDte03Request() : base() { }

    public CreateDte03Request(MhSettings mhSettings, ProcessingSettings processingSettings, string jwt, long? invoiceId, FeCcfv3 dte)
        : base(mhSettings, processingSettings, jwt, invoiceId)
    {
        Dte = dte;
    }

    public FeCcfv3 Dte { get; set; }
}
