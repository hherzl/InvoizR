using System.Collections.ObjectModel;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh.Anulacion;

namespace InvoizR.Application.Common.Contracts;

public interface ICancelDteRequest
{
    public Collection<ThirdPartyClientParameter> ThirdPartyClientParameters { get; }
    public long? InvoiceId { get; }
    public Anulacionv2 Anulacion { get; }
}
