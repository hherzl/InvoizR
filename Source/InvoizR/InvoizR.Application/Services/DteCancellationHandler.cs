using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Helpers;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.SharedKernel.Mh.Anulacion;
using Microsoft.Extensions.Options;

namespace InvoizR.Application.Services;

public sealed class DteCancellationHandler
{
    private readonly ProcessingSettings _processingSettings;
    private readonly IFirmadorClient _firmadorClient;
    private readonly IFesvClient _fesvClient;

    public DteCancellationHandler(IOptions<ProcessingSettings> processingOptions, IFirmadorClient firmadorClient, IFesvClient fesvClient)
    {
        _processingSettings = processingOptions.Value;
        _firmadorClient = firmadorClient;
        _fesvClient = fesvClient;
    }

    public async Task<bool> HandleAsync(ICancelDteRequest request, IInvoizRDbContext dbContext, CancellationToken st = default)
    {
        var invoice = await dbContext.GetInvoiceAsync(request.InvoiceId, true, true, st);

        if (!Directory.Exists(_processingSettings.GetLogsPath(invoice.AuditNumber)))
            Directory.CreateDirectory(_processingSettings.GetLogsPath(invoice.AuditNumber));

        _firmadorClient.ClientSettings = request.ThirdPartyClientParameters.GetByService(_firmadorClient.ServiceName).ToFirmadorClientSettings();

        var firmarDocumentoRequest = new FirmarDocumentoRequest<Anulacionv2>(request.ThirdPartyClientParameters.GetUser(), true, request.ThirdPartyClientParameters.GetPrivateKey(), request.Anulacion);
        await File.WriteAllTextAsync(_processingSettings.GetDteCancellationFirmaRequestJsonPath(invoice.AuditNumber), firmarDocumentoRequest.ToJson(), st);

        var firmarDocumentoResponse = await _firmadorClient.FirmarDocumentoAsync(firmarDocumentoRequest);
        await File.WriteAllTextAsync(_processingSettings.GetDteCancellationFirmaResponseJsonPath(invoice.AuditNumber), firmarDocumentoResponse.ToJson(), st);

        dbContext.InvoiceCancellationLog.Add(InvoiceCancellationLog.CreateRequest(invoice.Id, firmarDocumentoResponse.ToJson()));

        var anularDteRequest = new AnularDteRequest(invoice.Pos.Branch.Company.Environment, firmarDocumentoResponse.Body);
        await File.WriteAllTextAsync(_processingSettings.GetDteCancellationRequestJsonPath(invoice.AuditNumber), anularDteRequest.ToJson(), st);

        _fesvClient.Jwt = request.ThirdPartyClientParameters.GetToken();

        var anularDteResponse = await _fesvClient.AnularDteAsync(anularDteRequest);
        await File.WriteAllTextAsync(_processingSettings.GetDteCancellationResponseJsonPath(invoice.AuditNumber), anularDteResponse.ToJson(), st);

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
