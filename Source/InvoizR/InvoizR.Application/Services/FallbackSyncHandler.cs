using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Helpers;
using InvoizR.Application.Services.Models;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.SharedKernel.Mh.Contingencia;
using Microsoft.Extensions.Options;

namespace InvoizR.Application.Services;

public sealed class FallbackSyncHandler(IOptions<ProcessingSettings> processingOptions, IFirmadorClient firmadorClient, IFesvClient fesvClient)
{
    public async Task<bool> HandleAsync(CreateFallbackRequest request, IInvoizRDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var _processingSettings = processingOptions.Value;

        var fallback = await dbContext.GetFallbackAsync((short)request.InvoiceId, tracking: true, ct: cancellationToken);

        if (!Directory.Exists(_processingSettings.GetLogsPath(fallback.FallbackGuid)))
            Directory.CreateDirectory(_processingSettings.GetLogsPath(fallback.FallbackGuid));

        firmadorClient.ClientSettings = request.ThirdPartyClientParameters.GetByService(firmadorClient.ServiceName).ToFirmadorClientSettings();

        var firmarDocumentoRequest = new FirmarDocumentoRequest<Contingenciav3>(request.ThirdPartyClientParameters.GetUser(), true, request.ThirdPartyClientParameters.GetPrivateKey(), request.Dte);

        await File.WriteAllTextAsync(_processingSettings.GetFirmaRequestJsonPath(fallback.FallbackGuid), firmarDocumentoRequest.ToJson(), cancellationToken);

        var firmarDocumentoResponse = await firmadorClient.FirmarDocumentoAsync(firmarDocumentoRequest);
        await File.WriteAllTextAsync(_processingSettings.GetFirmaResponseJsonPath(fallback.FallbackGuid), firmarDocumentoResponse.ToJson(), cancellationToken);

        dbContext.FallbackProcessingLog.Add(FallbackProcessingLog.CreateRequest(fallback.Id, InvoiceProcessingStatus.Requested, firmarDocumentoResponse.ToJson()));

        var contingenciaRequest = new ContingenciaRequest("NIT", firmarDocumentoResponse.Body);
        await File.WriteAllTextAsync(_processingSettings.GetRecepcionRequestJsonPath(fallback.FallbackGuid), contingenciaRequest.ToJson(), cancellationToken);

        fesvClient.Jwt = request.ThirdPartyClientParameters.GetToken();

        var contingenciaResponse = await fesvClient.ContingenciaAsync(contingenciaRequest);
        await File.WriteAllTextAsync(_processingSettings.GetRecepcionResponseJsonPath(fallback.FallbackGuid), contingenciaResponse.ToJson(), cancellationToken);

        if (contingenciaResponse.IsSuccessful)
        {
            dbContext.FallbackProcessingLog.Add(FallbackProcessingLog.CreateResponse(fallback.Id, InvoiceProcessingStatus.Processed, contingenciaRequest.ToJson()));

            fallback.SyncStatusId = (short)InvoiceProcessingStatus.Processed;
            fallback.EmitDateTime = DateTime.Now;
            fallback.ReceiptStamp = contingenciaResponse.SelloRecibido;
        }
        else
        {
            dbContext.FallbackProcessingLog.Add(FallbackProcessingLog.CreateResponse(fallback.Id, InvoiceProcessingStatus.Declined, contingenciaResponse.ToJson()));

            fallback.SyncStatusId = (short)InvoiceProcessingStatus.Declined;
            fallback.SyncAttempts += 1;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return contingenciaResponse.IsSuccessful;
    }
}
