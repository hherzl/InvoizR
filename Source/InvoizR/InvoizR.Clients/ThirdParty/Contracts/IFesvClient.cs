using InvoizR.Clients.ThirdParty.DataContracts;

namespace InvoizR.Clients.ThirdParty.Contracts;

public interface IFeSvClient
{
    string ServiceName { get; }

    FesvClientSettings ClientSettings { get; set; }

    string Jwt { get; set; }

    Task<RecepcionDteResponse> RecepcionDteAsync(RecepcionDteRequest request);

    Task<ConsultaDteResponse> ConsultaDteAsync(ConsultaDteRequest request);

    Task<ContingenciaResponse> ContingenciaAsync(ContingenciaRequest request);

    Task<RecepcionDteResponse> AnularDteAsync(AnularDteRequest request);
}
