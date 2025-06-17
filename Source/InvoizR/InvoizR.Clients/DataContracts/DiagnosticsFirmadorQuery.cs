using MediatR;

namespace InvoizR.Clients.DataContracts;

public record DiagnosticsFirmadorQuery : IRequest<DiagnosticsFirmadorResponse>
{
}
