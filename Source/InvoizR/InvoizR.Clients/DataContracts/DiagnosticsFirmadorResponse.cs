using InvoizR.Clients.ThirdParty.DataContracts;
using MediatR;

namespace InvoizR.Clients.DataContracts;

public record DiagnosticsFirmadorResponse : IRequest<DiagnosticsFirmadorQuery>
{
    public DiagnosticsFirmadorResponse() { }

    public DiagnosticsFirmadorResponse(FirmarDocumentoResponse result)
    {
        Result = result;
    }

    public FirmarDocumentoResponse Result { get; set; }
}
