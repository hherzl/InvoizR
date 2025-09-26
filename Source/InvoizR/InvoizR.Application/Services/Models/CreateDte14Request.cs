using InvoizR.Application.Common.Contracts;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh.FeFse;

namespace InvoizR.Application.Services.Models;

public record CreateDte14Request : CreateDteRequest, ICreateDteRequest<FeFsev1>
{
    public static ICreateDteRequest<FeFsev1> Create(IEnumerable<ThirdPartyClientParameter> clientParameters, long? invoiceId, string payload)
        => new CreateDte14Request(clientParameters, invoiceId, FeFsev1.Deserialize(payload));

    public CreateDte14Request() : base() { }

    public CreateDte14Request(IEnumerable<ThirdPartyClientParameter> clientParameters, long? invoiceId, FeFsev1 dte) : base(clientParameters, invoiceId)
    {
        Dte = dte;
    }

    public FeFsev1 Dte { get; }
}
