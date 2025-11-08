using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
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

public sealed class InvoiceSyncHandler(IOptions<ProcessingSettings> processingOptions, IFirmadorClient firmadorClient, IFeSvClient feSvClient)
{
    private static void ReceiveInvoice(Invoice invoice, string eSignature)
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

    public async Task<bool> HandleAsync<TDte>(ICreateDteRequest<TDte> request, IInvoizRDbContext dbContext, CancellationToken ct = default) where TDte : Dte
    {
        var processingSettings = processingOptions.Value;

        var invoice = await dbContext.GetInvoiceAsync(request.InvoiceId, true, true, ct);

        if (!Directory.Exists(processingSettings.GetLogsPath(invoice.AuditNumber)))
            Directory.CreateDirectory(processingSettings.GetLogsPath(invoice.AuditNumber));

        firmadorClient.ClientSettings = request.ThirdPartyClientParameters.GetByService(firmadorClient.ServiceName).ToFirmadorClientSettings();

        var firmarDocumentoRequest = new FirmarDocumentoRequest<TDte>(request.ThirdPartyClientParameters.GetUser(), true, request.ThirdPartyClientParameters.GetPrivateKey(), request.Dte);

        await File.WriteAllTextAsync(processingSettings.GetFirmaRequestJsonPath(invoice.AuditNumber), firmarDocumentoRequest.ToJson(), ct);

        var firmarDocumentoResponse = await firmadorClient.FirmarDocumentoAsync(firmarDocumentoRequest);
        await File.WriteAllTextAsync(processingSettings.GetFirmaResponseJsonPath(invoice.AuditNumber), firmarDocumentoResponse.ToJson(), ct);

        dbContext.InvoiceProcessingLog.Add(
            InvoiceProcessingLog.CreateRequest(invoice.Id, InvoiceProcessingStatus.Requested, firmarDocumentoResponse.ToJson())
        );

        feSvClient.ClientSettings = request.ThirdPartyClientParameters.GetByService(feSvClient.ServiceName).ToFesvClientSettings();

        var recepcionRequest = new RecepcionDteRequest(invoice.Pos.Branch.Company.Environment, invoice.SchemaVersion, invoice.SchemaType, invoice.InvoiceGuid, firmarDocumentoResponse.Body);
        await File.WriteAllTextAsync(processingSettings.GetRecepcionRequestJsonPath(invoice.AuditNumber), recepcionRequest.ToJson(), ct);

        feSvClient.Jwt = request.ThirdPartyClientParameters.GetToken();

        var recepcionResponse = await feSvClient.RecepcionDteAsync(recepcionRequest);

        await File.WriteAllTextAsync(processingSettings.GetRecepcionResponseJsonPath(invoice.AuditNumber), recepcionResponse.ToJson(), ct);

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

        await dbContext.SaveChangesAsync(ct);

        return recepcionResponse.IsSuccessful;
    }
}
