using InvoizR.Clients.DataContracts;

namespace InvoizR.Clients.Contracts;

public interface IDiagnosticsClient
{
    DiagnosticsClientSettings ClientSettings { get; }

    Task<DiagnosticsSeguridadResponse> DiagnosticsSeguridadAsync(short id);

    Task<DiagnosticsFirmadorResponse> DiagnosticsFirmadorAsync(short id);
}
