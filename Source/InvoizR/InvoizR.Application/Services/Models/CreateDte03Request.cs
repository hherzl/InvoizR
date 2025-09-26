using InvoizR.Application.Common.Contracts;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh.FeCcf;

namespace InvoizR.Application.Services.Models;

public record CreateDte03Request : CreateDteRequest, ICreateDteRequest<FeCcfv3>
{
    public static ICreateDteRequest<FeCcfv3> Create(IEnumerable<ThirdPartyClientParameter> clientParameters, long? invoiceId, string payload)
        => new CreateDte03Request(clientParameters, invoiceId, FeCcfv3.Deserialize(payload));

    public CreateDte03Request() : base() { }

    public CreateDte03Request(IEnumerable<ThirdPartyClientParameter> clientParameters, long? invoiceId, FeCcfv3 dte) : base(clientParameters, invoiceId)
    {
        Dte = dte;
    }

    public FeCcfv3 Dte { get; set; }
}
