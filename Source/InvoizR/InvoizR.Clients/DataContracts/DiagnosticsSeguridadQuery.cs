using MediatR;

namespace InvoizR.Clients.DataContracts;

public record DiagnosticsSeguridadQuery : IRequest<DiagnosticsSeguridadResponse>
{
    public DiagnosticsSeguridadQuery(short? id)
    {
        CompanyId = id;
    }

    public short? CompanyId { get; }
}
