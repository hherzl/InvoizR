using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Helpers;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.SharedKernel.Mh;
using InvoizR.SharedKernel.Mh.FeCcf;
using InvoizR.SharedKernel.Mh.FeFc;
using InvoizR.SharedKernel.Mh.FeFse;
using InvoizR.SharedKernel.Mh.FeNc;
using InvoizR.SharedKernel.Mh.FeNd;
using InvoizR.SharedKernel.Mh.FeNr;
using Microsoft.Extensions.Options;

namespace InvoizR.Application.Services;

public class DteSyncHandler
{
    private readonly ProcessingSettings _processingSettings;
    private readonly IFirmadorClient _firmadorClient;
    private readonly IFesvClient _fesvClient;

    public DteSyncHandler(IOptions<ProcessingSettings> processingOptions, IFirmadorClient firmadorClient, IFesvClient fesvClient)
    {
        _processingSettings = processingOptions.Value;
        _firmadorClient = firmadorClient;
        _fesvClient = fesvClient;
    }

    private void ReceiveInvoice(Invoice invoice, string eSignature)
    {
        if (invoice.InvoiceTypeId == FeFcv1.TypeId)
        {
            var receivedInvoice = FeFcv1Received.DeserializeReceived(invoice.Payload);
            receivedInvoice.SelloRecibido = invoice.ReceiptStamp;
            receivedInvoice.FirmaElectronica = eSignature;

            invoice.Payload = receivedInvoice.ToJson();
        }
        else if (invoice.InvoiceTypeId == FeCcfv3.TypeId)
        {
            var receivedInvoice = FeCcfv3Received.DeserializeReceived(invoice.Payload);
            receivedInvoice.SelloRecibido = invoice.ReceiptStamp;
            receivedInvoice.FirmaElectronica = eSignature;

            invoice.Payload = receivedInvoice.ToJson();
        }
        else if (invoice.InvoiceTypeId == FeNrv3.TypeId)
        {
            var receivedInvoice = FeNrv3Received.DeserializeReceived(invoice.Payload);
            receivedInvoice.SelloRecibido = invoice.ReceiptStamp;
            receivedInvoice.FirmaElectronica = eSignature;

            invoice.Payload = receivedInvoice.ToJson();
        }
        else if (invoice.InvoiceTypeId == FeNcv3.TypeId)
        {
            var receivedInvoice = FeNcv3Received.DeserializeReceived(invoice.Payload);
            receivedInvoice.SelloRecibido = invoice.ReceiptStamp;
            receivedInvoice.FirmaElectronica = eSignature;

            invoice.Payload = receivedInvoice.ToJson();
        }
        else if (invoice.InvoiceTypeId == FeNdv3.TypeId)
        {
            var receivedInvoice = FeNdv3Received.DeserializeReceived(invoice.Payload);
            receivedInvoice.SelloRecibido = invoice.ReceiptStamp;
            receivedInvoice.FirmaElectronica = eSignature;

            invoice.Payload = receivedInvoice.ToJson();
        }
        else if (invoice.InvoiceTypeId == FeFsev1.TypeId)
        {
            var receivedInvoice = FeFsev1Received.DeserializeReceived(invoice.Payload);
            receivedInvoice.SelloRecibido = invoice.ReceiptStamp;
            receivedInvoice.FirmaElectronica = eSignature;

            invoice.Payload = receivedInvoice.ToJson();
        }
    }

    public async Task<bool> HandleAsync<TDte>(ICreateDteRequest<TDte> request, IInvoizRDbContext dbContext, CancellationToken cancellationToken = default) where TDte : Dte
    {
        var invoice = await dbContext.GetInvoiceAsync(request.InvoiceId, true, true, cancellationToken);

        if (!Directory.Exists(_processingSettings.GetLogsPath(invoice.AuditNumber)))
            Directory.CreateDirectory(_processingSettings.GetLogsPath(invoice.AuditNumber));

        _firmadorClient.ClientSettings = request.ThirdPartyClientParameters.GetByService(_firmadorClient.ServiceName).ToFirmadorClientSettings();

        var firmarDocumentoRequest = new FirmarDocumentoRequest<TDte>(request.ThirdPartyClientParameters.GetUser(), true, request.ThirdPartyClientParameters.GetPrivateKey(), request.Dte);

        await File.WriteAllTextAsync(_processingSettings.GetFirmaRequestJsonPath(invoice.AuditNumber), firmarDocumentoRequest.ToJson(), cancellationToken);

        var firmarDocumentoResponse = await _firmadorClient.FirmarDocumentoAsync(firmarDocumentoRequest);
        await File.WriteAllTextAsync(_processingSettings.GetFirmaResponseJsonPath(invoice.AuditNumber), firmarDocumentoResponse.ToJson(), cancellationToken);

        dbContext.InvoiceProcessingLog.Add(
            InvoiceProcessingLog.CreateRequest(invoice.Id, InvoiceProcessingStatus.Requested, firmarDocumentoResponse.ToJson())
        );

        var recepcionRequest = new RecepcionDteRequest(invoice.Pos.Branch.Company.Environment, invoice.SchemaVersion, invoice.SchemaType, invoice.InvoiceGuid, firmarDocumentoResponse.Body);
        await File.WriteAllTextAsync(_processingSettings.GetRecepcionRequestJsonPath(invoice.AuditNumber), recepcionRequest.ToJson(), cancellationToken);

        _fesvClient.Jwt = request.ThirdPartyClientParameters.GetToken();

        var recepcionResponse = await _fesvClient.RecepcionDteAsync(recepcionRequest);

        await File.WriteAllTextAsync(_processingSettings.GetRecepcionResponseJsonPath(invoice.AuditNumber), recepcionResponse.ToJson(), cancellationToken);

        if (recepcionResponse.IsSuccessful)
        {
            dbContext.InvoiceProcessingLog.Add(
                InvoiceProcessingLog.CreateResponse(invoice.Id, InvoiceProcessingStatus.Processed, recepcionRequest.ToJson())
            );

            invoice.ProcessingStatusId = (short)InvoiceProcessingStatus.Processed;
            invoice.EmitDateTime = DateTime.Now;
            invoice.ReceiptStamp = recepcionResponse.SelloRecibido;
            invoice.ExternalUrl = request.ThirdPartyClientParameters.GetPublicQuery()
                .Replace("env", invoice.Pos.Branch.Company.Environment)
                .Replace("guid", invoice.InvoiceGuid)
                .Replace("emitDate", invoice.InvoiceDate?.ToString("yyyy-MM-dd"))
                ;

            dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));

            ReceiveInvoice(invoice, firmarDocumentoResponse.Body);
        }
        else
        {
            dbContext.InvoiceProcessingLog.Add(
                InvoiceProcessingLog.CreateResponse(invoice.Id, InvoiceProcessingStatus.Declined, recepcionRequest.ToJson())
            );

            invoice.ProcessingStatusId = (short)InvoiceProcessingStatus.Declined;
            invoice.SyncAttempts += 1;

            dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return recepcionResponse.IsSuccessful;
    }
}
