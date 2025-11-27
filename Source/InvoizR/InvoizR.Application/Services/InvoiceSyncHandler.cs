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

public sealed class InvoiceSyncHandler(IOptions<ProcessingSettings> processingOptions, IFirmadorClient firmadorClient, IFeSvClient feSvClient, WebhookNotificationHandler webhookNotificationHandler)
{
    public async Task<bool> HandleAsync<TInvoice>(ICreateDteRequest<TInvoice> request, IInvoizRDbContext dbContext, CancellationToken ct = default) where TInvoice : Dte
    {
        var processingSettings = processingOptions.Value;

        var invoice = await dbContext.GetInvoiceAsync(request.InvoiceId, true, true, ct);

        if (!Directory.Exists(processingSettings.GetLogsPath(invoice.AuditNumber)))
            Directory.CreateDirectory(processingSettings.GetLogsPath(invoice.AuditNumber));

        firmadorClient.ClientSettings = request.ThirdPartyClientParameters.GetByService(firmadorClient.ServiceName).ToFirmadorClientSettings();

        var firmarDocumentoRequest = new FirmarDocumentoRequest<TInvoice>(request.ThirdPartyClientParameters.GetUser(), true, request.ThirdPartyClientParameters.GetPrivateKey(), request.Dte);

        await File.WriteAllTextAsync(processingSettings.GetFirmaRequestJsonPath(invoice.AuditNumber), firmarDocumentoRequest.ToJson(), ct);

        var firmarDocumentoResponse = await firmadorClient.FirmarDocumentoAsync(firmarDocumentoRequest);
        await File.WriteAllTextAsync(processingSettings.GetFirmaResponseJsonPath(invoice.AuditNumber), firmarDocumentoResponse.ToJson(), ct);

        dbContext.InvoiceSyncLog.Add(InvoiceSyncLog.CreateRequest(invoice.Id, SyncStatus.Requested, firmarDocumentoResponse.ToJson()));

        feSvClient.ClientSettings = request.ThirdPartyClientParameters.GetByService(feSvClient.ServiceName).ToFesvClientSettings();

        var recepcionRequest = new RecepcionDteRequest(invoice.Pos.Branch.Company.Environment, invoice.SchemaVersion, invoice.SchemaType, invoice.InvoiceGuid, firmarDocumentoResponse.Body);
        await File.WriteAllTextAsync(processingSettings.GetRecepcionRequestJsonPath(invoice.AuditNumber), recepcionRequest.ToJson(), ct);

        feSvClient.Jwt = request.ThirdPartyClientParameters.GetToken();

        var recepcionResponse = await feSvClient.RecepcionDteAsync(recepcionRequest);

        await File.WriteAllTextAsync(processingSettings.GetRecepcionResponseJsonPath(invoice.AuditNumber), recepcionResponse.ToJson(), ct);

        if (recepcionResponse.IsSuccessful)
        {
            dbContext.InvoiceSyncLog.Add(InvoiceSyncLog.CreateResponse(invoice.Id, SyncStatus.Processed, recepcionRequest.ToJson()));

            invoice.SyncStatusId = (short)SyncStatus.Processed;
            invoice.EmitDateTime = DateTime.Now;
            invoice.ReceiptStamp = recepcionResponse.SelloRecibido;
            invoice.ExternalUrl = request.ThirdPartyClientParameters.GetPublicQuery()
                .Replace("env", invoice.Pos.Branch.Company.Environment)
                .Replace("guid", invoice.InvoiceGuid)
                .Replace("emitDate", invoice.InvoiceDate?.ToString("yyyy-MM-dd"))
                ;

            dbContext.InvoiceSyncStatusLog.Add(new(invoice.Id, invoice.SyncStatusId));

            ReceiveInvoice(invoice, firmarDocumentoResponse.Body);

            if (invoice.Pos.Branch.Company.HasWebhook)
                await webhookNotificationHandler.HandleAsync(new(invoice), ct);
        }
        else
        {
            dbContext.InvoiceSyncLog.Add(InvoiceSyncLog.CreateResponse(invoice.Id, SyncStatus.Declined, recepcionRequest.ToJson()));

            invoice.SyncStatusId = (short)SyncStatus.Declined;
            invoice.SyncAttempts += 1;

            dbContext.InvoiceSyncStatusLog.Add(new(invoice.Id, invoice.SyncStatusId));
        }

        await dbContext.SaveChangesAsync(ct);

        return recepcionResponse.IsSuccessful;
    }

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
        else
            throw new NotImplementedException($"There is no implementation for {invoice.InvoiceTypeId} invoice type");
    }
}
