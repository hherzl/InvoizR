using InvoizR.Clients.ThirdParty.DataContracts;
using MediatR;

namespace InvoizR.Clients.DataContracts;

public record DiagnosticsSeguridadResponse : IRequest<DiagnosticsSeguridadQuery>
{
    public DiagnosticsSeguridadResponse() { }

    public DiagnosticsSeguridadResponse(AuthResponse result)
    {
        Result = result;
    }

    public AuthResponse Result { get; set; }
}
