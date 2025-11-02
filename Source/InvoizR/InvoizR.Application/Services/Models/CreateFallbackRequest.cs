using InvoizR.Application.Common.Contracts;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh.Contingencia;

namespace InvoizR.Application.Services.Models;

public record CreateFallbackRequest : CreateDteRequest, ICreateDteRequest<Contingenciav3>
{
    public CreateFallbackRequest()
        : base()
    {
    }

    public CreateFallbackRequest(IEnumerable<ThirdPartyClientParameter> clientParameters, long? invoiceId, string payload)
        : base(clientParameters, invoiceId)
    {
        Dte = Contingenciav3.Deserialize(payload);
    }

    public Contingenciav3 Dte { get; }
}
