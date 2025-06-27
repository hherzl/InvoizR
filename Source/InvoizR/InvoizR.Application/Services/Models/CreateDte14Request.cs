using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.SharedKernel.Mh.FeFse;

namespace InvoizR.Application.Services.Models;

public record CreateDte14Request : CreateDteRequest, ICreateDteRequest<FeFsev1>
{
    public static ICreateDteRequest<FeFsev1> Create(MhSettings mhSettings, ProcessingSettings processingSettings, string jwt, long? invoiceId, string payload)
        => new CreateDte14Request(mhSettings, processingSettings, jwt, invoiceId, FeFsev1.Deserialize(payload));

    public CreateDte14Request() : base() { }

    public CreateDte14Request(MhSettings mhSettings, ProcessingSettings processingSettings, string jwt, long? invoiceId, FeFsev1 dte)
        : base(mhSettings, processingSettings, jwt, invoiceId)
    {
        Dte = dte;
    }

    public FeFsev1 Dte { get; }
}
