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
    public async Task<bool> HandleAsync(ICancelDteRequest request, IInvoizRDbContext dbContext, CancellationToken st = default)
    {
        var _processingSettings = processingOptions.Value;

        var invoice = await dbContext.GetInvoiceAsync(request.InvoiceId, true, true, st);

        if (!Directory.Exists(_processingSettings.GetLogsPath(invoice.AuditNumber)))
            Directory.CreateDirectory(_processingSettings.GetLogsPath(invoice.AuditNumber));

        firmadorClient.ClientSettings = request.ThirdPartyClientParameters.GetByService(firmadorClient.ServiceName).ToFirmadorClientSettings();

        var firmarDocumentoRequest = new FirmarDocumentoRequest<Anulacionv2>(request.ThirdPartyClientParameters.GetUser(), true, request.ThirdPartyClientParameters.GetPrivateKey(), request.Anulacion);
        await File.WriteAllTextAsync(_processingSettings.GetInvoiceCancellationFirmaRequestJsonPath(invoice.AuditNumber), firmarDocumentoRequest.ToJson(), st);

        var firmarDocumentoResponse = await firmadorClient.FirmarDocumentoAsync(firmarDocumentoRequest);
        await File.WriteAllTextAsync(_processingSettings.GetInvoiceCancellationFirmaResponseJsonPath(invoice.AuditNumber), firmarDocumentoResponse.ToJson(), st);

        dbContext.InvoiceCancellationLog.Add(InvoiceCancellationLog.CreateRequest(invoice.Id, firmarDocumentoResponse.ToJson()));

        var anularDteRequest = new AnularDteRequest(invoice.Pos.Branch.Company.Environment, firmarDocumentoResponse.Body);
        await File.WriteAllTextAsync(_processingSettings.GetInvoiceCancellationRequestJsonPath(invoice.AuditNumber), anularDteRequest.ToJson(), st);

        feSvClient.Jwt = request.ThirdPartyClientParameters.GetToken();

        var anularDteResponse = await feSvClient.AnularDteAsync(anularDteRequest);
        await File.WriteAllTextAsync(_processingSettings.GetInvoiceCancellationResponseJsonPath(invoice.AuditNumber), anularDteResponse.ToJson(), st);

        if (anularDteResponse.IsSuccessful)
        {
            dbContext.InvoiceCancellationLog.Add(InvoiceCancellationLog.CreateResponse(invoice.Id, anularDteResponse.ToJson(), InvoiceProcessingStatus.Processed));

            invoice.CancellationProcessingStatusId = (short)InvoiceProcessingStatus.Processed;
        }
        else
        {
            dbContext.InvoiceCancellationLog.Add(InvoiceCancellationLog.CreateResponse(invoice.Id, anularDteResponse.ToJson(), InvoiceProcessingStatus.Declined));

            invoice.CancellationProcessingStatusId = (short)InvoiceProcessingStatus.Declined;
        }

        await dbContext.SaveChangesAsync(st);

        return anularDteResponse.IsSuccessful;
    }
}
