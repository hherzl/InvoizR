using InvoizR.Application.Common.Contracts;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh.FeNr;

namespace InvoizR.Application.Services.Models;

public record CreateDte04Request : CreateDteRequest, ICreateDteRequest<FeNrv3>
{
    public static ICreateDteRequest<FeNrv3> Create(IEnumerable<ThirdPartyClientParameter> clientParameters, long? invoiceId, string payload)
        => new CreateDte04Request(clientParameters, invoiceId, FeNrv3.Deserialize(payload));

    public CreateDte04Request() : base() { }

    public CreateDte04Request(IEnumerable<ThirdPartyClientParameter> clientParameters, long? invoiceId, FeNrv3 dte) : base(clientParameters, invoiceId)
    {
        Dte = dte;
    }

    public FeNrv3 Dte { get; }
}
