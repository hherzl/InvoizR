using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.SharedKernel.Mh.FeFc;

namespace InvoizR.Application.Services.Models;

public record CreateDte01Request : CreateDteRequest, ICreateDteRequest<FeFcv1>
{
    public static ICreateDteRequest<FeFcv1> Create(MhSettings mhSettings, ProcessingSettings processingSettings, string jwt, long? invoiceId, string payload)
        => new CreateDte01Request(mhSettings, processingSettings, jwt, invoiceId, FeFcv1.Deserialize(payload));

    public CreateDte01Request() : base() { }

    public CreateDte01Request(MhSettings mhSettings, ProcessingSettings processingSettings, string jwt, long? invoiceId, FeFcv1 dte)
        : base(mhSettings, processingSettings, jwt, invoiceId)
    {
        Dte = dte;
    }

    public FeFcv1 Dte { get; }
}
