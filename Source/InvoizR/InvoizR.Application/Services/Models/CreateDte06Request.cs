using InvoizR.Application.Common.Contracts;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh.FeNd;

namespace InvoizR.Application.Services.Models;

public record CreateDte06Request : CreateDteRequest, ICreateDteRequest<FeNdv3>
{
    public static ICreateDteRequest<FeNdv3> Create(IEnumerable<ThirdPartyClientParameter> clientParameters, long? invoiceId, string payload)
        => new CreateDte06Request(clientParameters, invoiceId, FeNdv3.Deserialize(payload));

    public CreateDte06Request() : base() { }

    public CreateDte06Request(IEnumerable<ThirdPartyClientParameter> clientParameters, long? invoiceId, FeNdv3 dte)
        : base(clientParameters, invoiceId)
    {
        Dte = dte;
    }

    public FeNdv3 Dte { get; }
}
