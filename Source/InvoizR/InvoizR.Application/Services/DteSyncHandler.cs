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
using InvoizR.SharedKernel.Mh.FeNr;

namespace InvoizR.Application.Services;

public class DteSyncHandler
{
    private readonly IFirmadorClient _firmadorClient;
    private readonly IFesvClient _fesvClient;
    private readonly string _externalUrl;

    public DteSyncHandler(IFirmadorClient firmadorClient, IFesvClient fesvClient)
    {
        _firmadorClient = firmadorClient;
        _fesvClient = fesvClient;
        _externalUrl = "https://admin.factura.gob.sv/consultaPublica?ambiente=env&codGen=genCode&fechaEmi=invDate";
    }

    string GetExternalUrl(string env, string genCode, DateTime? invDate)
        => _externalUrl.Replace("env", env).Replace("genCode", genCode).Replace("invDate", invDate?.ToString("yyyy-MM-dd"));

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
        else if (invoice.InvoiceTypeId == FeFsev1.TypeId)
        {
            var receivedInvoice = FeFsev1Received.DeserializeReceived(invoice.Payload);
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
    }

    public async Task<bool> HandleAsync<TDte>(ICreateDteRequest<TDte> request, IInvoizRDbContext dbContext, CancellationToken cancellationToken = default) where TDte : Dte
    {
        var invoice = await dbContext.GetInvoiceAsync(request.InvoiceId, true, true, cancellationToken);

        if (!Directory.Exists(request.ProcessingSettings.GetLogsPath(invoice.ControlNumber)))
            Directory.CreateDirectory(request.ProcessingSettings.GetLogsPath(invoice.ControlNumber));

        var firmarDocumentoReq = new FirmarDocumentoRequest<TDte>(request.MhSettings.User, true, request.MhSettings.PrivateKey, request.Dte);

        await File.WriteAllTextAsync(
            request.ProcessingSettings.GetFirmaRequestJsonPath(invoice.ControlNumber), firmarDocumentoReq.ToJson(), cancellationToken
        );

        var firmarDocumentoRes = await _firmadorClient.FirmarDocumentoAsync(firmarDocumentoReq);

        await File.WriteAllTextAsync(
            request.ProcessingSettings.GetFirmaResponseJsonPath(invoice.ControlNumber), firmarDocumentoRes.ToJson(), cancellationToken
        );

        dbContext.InvoiceProcessingLog.Add(
            InvoiceProcessingLog.CreateRequest(invoice.Id, InvoiceProcessingStatus.Requested, firmarDocumentoRes.ToJson())
        );

        var recepcionReq = new RecepcionDteRequest(
            request.MhSettings.Environment, invoice.SchemaVersion, invoice.SchemaType, invoice.GenerationCode, firmarDocumentoRes.Body
        );

        await File.WriteAllTextAsync(
            request.ProcessingSettings.GetRecepcionRequestJsonPath(invoice.ControlNumber), recepcionReq.ToJson(), cancellationToken
        );

        _fesvClient.Jwt = request.Jwt;

        var recepcionRes = await _fesvClient.RecepcionDteAsync(recepcionReq);

        await File.WriteAllTextAsync(
            request.ProcessingSettings.GetRecepcionResponseJsonPath(invoice.ControlNumber), recepcionRes.ToJson(), cancellationToken
        );

        if (recepcionRes.IsSuccessful)
        {
            dbContext.InvoiceProcessingLog.Add(
                InvoiceProcessingLog.CreateResponse(invoice.Id, InvoiceProcessingStatus.Processed, recepcionReq.ToJson())
            );

            invoice.ProcessingStatusId = (short)InvoiceProcessingStatus.Processed;
            invoice.ProcessingDateTime = DateTime.Now;
            invoice.ReceiptStamp = recepcionRes.SelloRecibido;
            invoice.ExternalUrl = GetExternalUrl(invoice.Pos.Branch.Company.Environment, invoice.GenerationCode, invoice.InvoiceDate);

            dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));

            ReceiveInvoice(invoice, firmarDocumentoRes.Body);
        }
        else
        {
            dbContext.InvoiceProcessingLog.Add(
                InvoiceProcessingLog.CreateResponse(invoice.Id, InvoiceProcessingStatus.Declined, recepcionReq.ToJson())
            );

            invoice.ProcessingStatusId = (short)InvoiceProcessingStatus.Declined;
            invoice.SyncAttempts += 1;

            dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return recepcionRes.IsSuccessful;
    }
}
