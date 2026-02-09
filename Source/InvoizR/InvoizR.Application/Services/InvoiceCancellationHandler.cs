using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.SharedKernel.Mh.Anulacion;
using Microsoft.Extensions.Options;

namespace InvoizR.Application.Services;

public sealed class InvoiceCancellationHandler(IOptions<ProcessingSettings> processingOptions, IFirmadorClient firmadorClient, IFeSvClient feSvClient)
{
    public async Task<bool> HandleAsync(ICancelDteRequest request, IInvoizRDbContext dbContext, CancellationToken ct = default)
    {
        var processingSettings = processingOptions.Value;

        var invoice = await dbContext.GetInvoiceAsync(request.InvoiceId, true, true, ct);

        if (!Directory.Exists(processingSettings.GetLogsPath(invoice.AuditNumber)))
            Directory.CreateDirectory(processingSettings.GetLogsPath(invoice.AuditNumber));

        firmadorClient.ClientSettings = request.ThirdPartyClientParameters.GetByService(firmadorClient.ServiceName).ToFirmadorClientSettings();

        var firmarDocumentoRequest = new FirmarDocumentoRequest<Anulacionv2>(request.ThirdPartyClientParameters.GetUser(), true, request.ThirdPartyClientParameters.GetPrivateKey(), request.Anulacion);
        await File.WriteAllTextAsync(processingSettings.GetInvoiceCancellationFirmaRequestJsonPath(invoice.AuditNumber), firmarDocumentoRequest.ToJson(), ct);

        var firmarDocumentoResponse = await firmadorClient.FirmarDocumentoAsync(firmarDocumentoRequest);
        await File.WriteAllTextAsync(processingSettings.GetInvoiceCancellationFirmaResponseJsonPath(invoice.AuditNumber), firmarDocumentoResponse.ToJson(), ct);

        dbContext.InvoiceCancellationLogs.Add(InvoiceCancellationLog.CreateRequest(invoice.Id, firmarDocumentoResponse.ToJson()));

        var anularDteRequest = new AnularDteRequest(invoice.Pos.Branch.Company.Environment, firmarDocumentoResponse.Body);
        await File.WriteAllTextAsync(processingSettings.GetInvoiceCancellationRequestJsonPath(invoice.AuditNumber), anularDteRequest.ToJson(), ct);

        feSvClient.Jwt = request.ThirdPartyClientParameters.GetToken();

        var anularDteResponse = await feSvClient.AnularDteAsync(anularDteRequest);
        await File.WriteAllTextAsync(processingSettings.GetInvoiceCancellationResponseJsonPath(invoice.AuditNumber), anularDteResponse.ToJson(), ct);

        if (anularDteResponse.IsSuccessful)
        {
            dbContext.InvoiceCancellationLogs.Add(InvoiceCancellationLog.CreateResponse(invoice.Id, anularDteResponse.ToJson(), SyncStatus.Processed));

            invoice.CancellationProcessingStatusId = (short)SyncStatus.Processed;
        }
        else
        {
            dbContext.InvoiceCancellationLogs.Add(InvoiceCancellationLog.CreateResponse(invoice.Id, anularDteResponse.ToJson(), SyncStatus.Declined));

            invoice.CancellationProcessingStatusId = (short)SyncStatus.Declined;
        }

        await dbContext.SaveChangesAsync(ct);

        return anularDteResponse.IsSuccessful;
    }
}
