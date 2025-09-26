using MediatR;

namespace InvoizR.Clients.DataContracts;

public record DiagnosticsFirmadorQuery : IRequest<DiagnosticsFirmadorResponse>
{
    public DiagnosticsFirmadorQuery(short? id)
    {
        CompanyId = id;
    }

    public short? CompanyId { get; }
}
