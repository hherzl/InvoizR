using InvoizR.Clients.ThirdParty.DataContracts;
using InvoizR.SharedKernel.Mh;

namespace InvoizR.Clients.ThirdParty.Contracts;

public interface IFirmadorClient
{
    string ServiceName { get; }

    FirmadorClientSettings ClientSettings { get; set; }

    Task<string> StatusAsync();

    Task<FirmarDocumentoResponse> FirmarDocumentoAsync<TDte>(FirmarDocumentoRequest<TDte> request) where TDte : Dte;
}
