using InvoizR.SharedKernel.Mh;

namespace InvoizR.Application.Common.Contracts;

public interface ICreateDteRequest<TDte> where TDte : Dte
{
    public MhSettings MhSettings { get; set; }
    public ProcessingSettings ProcessingSettings { get; set; }
    public string Jwt { get; set; }
    public long? InvoiceId { get; set; }
    public TDte Dte { get; }
}
