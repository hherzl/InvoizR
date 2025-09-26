using InvoizR.Application.Common.Contracts;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh.FeFc;

namespace InvoizR.Application.Services.Models;

public record CreateDte01Request : CreateDteRequest, ICreateDteRequest<FeFcv1>
{
    public static ICreateDteRequest<FeFcv1> Create(IEnumerable<ThirdPartyClientParameter> clientParameters, long? invoiceId, string payload)
        => new CreateDte01Request(clientParameters, invoiceId, FeFcv1.Deserialize(payload));

    public CreateDte01Request() : base() { }

    public CreateDte01Request(IEnumerable<ThirdPartyClientParameter> clientParameters, long? invoiceId, FeFcv1 dte) : base(clientParameters, invoiceId)
    {
        Dte = dte;
    }

    public FeFcv1 Dte { get; }
}
