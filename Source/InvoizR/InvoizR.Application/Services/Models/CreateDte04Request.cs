using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.SharedKernel.Mh.FeNr;

namespace InvoizR.Application.Services.Models;

public record CreateDte04Request : CreateDteRequest, ICreateDteRequest<FeNrv3>
{
    public static ICreateDteRequest<FeNrv3> Create(MhSettings mhSettings, ProcessingSettings processingSettings, string jwt, long? invoiceId, string payload)
        => new CreateDte04Request(mhSettings, processingSettings, jwt, invoiceId, FeNrv3.Deserialize(payload));

    public CreateDte04Request() : base() { }

    public CreateDte04Request(MhSettings mhSettings, ProcessingSettings processingSettings, string jwt, long? invoiceId, FeNrv3 dte)
        : base(mhSettings, processingSettings, jwt, invoiceId)
    {
        Dte = dte;
    }

    public FeNrv3 Dte { get; }
}
