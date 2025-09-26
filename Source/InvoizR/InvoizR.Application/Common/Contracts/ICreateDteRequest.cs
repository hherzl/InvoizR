using System.Collections.ObjectModel;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh;

namespace InvoizR.Application.Common.Contracts;

public interface ICreateDteRequest<TDte> where TDte : Dte
{
    public Collection<ThirdPartyClientParameter> ThirdPartyClientParameters { get; }
    public ProcessingSettings ProcessingSettings { get; set; }
    public long? InvoiceId { get; set; }
    public TDte Dte { get; }
}
