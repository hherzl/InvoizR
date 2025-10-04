using InvoizR.Application.Common.Contracts;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh.FeNc;

namespace InvoizR.Application.Services.Models;

public record CreateDte05Request : CreateDteRequest, ICreateDteRequest<FeNcv3>
{
    public static ICreateDteRequest<FeNcv3> Create(IEnumerable<ThirdPartyClientParameter> clientParameters, long? invoiceId, string payload)
        => new CreateDte05Request(clientParameters, invoiceId, FeNcv3.Deserialize(payload));

    public CreateDte05Request() : base() { }

    public CreateDte05Request(IEnumerable<ThirdPartyClientParameter> clientParameters, long? invoiceId, FeNcv3 dte)
        : base(clientParameters, invoiceId)
    {
        Dte = dte;
    }

    public FeNcv3 Dte { get; }
}
