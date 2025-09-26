using System.Collections.ObjectModel;
using InvoizR.Application.Common;
using InvoizR.SharedKernel;

namespace InvoizR.Application.Services.Models;

public record CreateDteRequest
{
    public CreateDteRequest()
    {
    }

    public CreateDteRequest(IEnumerable<ThirdPartyClientParameter> thirdPartyClientParameters, long? invoiceId)
        : base()
    {
        ThirdPartyClientParameters = new([.. thirdPartyClientParameters]);
        InvoiceId = invoiceId;
    }

    public Collection<ThirdPartyClientParameter> ThirdPartyClientParameters { get; protected set; }

    public ProcessingSettings ProcessingSettings { get; set; }
    public long? InvoiceId { get; set; }
}
