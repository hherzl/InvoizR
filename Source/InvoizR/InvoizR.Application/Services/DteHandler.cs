using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Helpers;
using InvoizR.Application.Services.Models;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.SharedKernel.Mh;
using Microsoft.Extensions.Configuration;

namespace InvoizR.Application.Services;

public class DteHandler
{
    private readonly IConfiguration _configuration;
    private readonly IFirmadorClient _firmadorClient;
    private readonly IFesvClient _fesvClient;
    private readonly string _externalUrl;

    public DteHandler(IConfiguration configuration, IFirmadorClient firmadorClient, IFesvClient fesvClient)
    {
        _configuration = configuration;
        _firmadorClient = firmadorClient;
        _fesvClient = fesvClient;
        _externalUrl = "https://admin.factura.gob.sv/consultaPublica?ambiente=env&codGen=genCode&fechaEmi=invDate";
    }

    string GetExternalUrl(string env, string genCode, DateTime? invDate)
        => _externalUrl.Replace("env", env).Replace("genCode", genCode).Replace("invDate", invDate?.ToString("yyyy-MM-dd"));

    public async Task<bool> HandleAsync(CreateDte01Request request, IInvoizRDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var inv = await dbContext.GetInvoiceAsync(request.InvoiceId, true, true, cancellationToken);

        if (!Directory.Exists(request.ProcessingSettings.GetLogsPath(inv.ControlNumber)))
            Directory.CreateDirectory(request.ProcessingSettings.GetLogsPath(inv.ControlNumber));

        var firmarDocumentoReq = new FirmarDocumentoRequest<FeFcv1>
        {
            Nit = request.MhSettings.User,
            Activo = true,
            PasswordPri = request.MhSettings.PrivateKey,
            DteJson = request.Dte
        };

        await File.WriteAllTextAsync(
            request.ProcessingSettings.GetFirmaRequestJsonPath(inv.ControlNumber), firmarDocumentoReq.ToJson(), cancellationToken
        );

        var firmarDocumentoRes = await _firmadorClient.FirmarDocumentoAsync(firmarDocumentoReq);

        await File.WriteAllTextAsync(
            request.ProcessingSettings.GetFirmaResponseJsonPath(inv.ControlNumber), firmarDocumentoRes.ToJson(), cancellationToken
        );

        dbContext.InvoiceProcessingLog.Add(
            InvoiceProcessingLog.CreateRequest(inv.Id, InvoiceProcessingStatus.Requested, firmarDocumentoRes.ToJson())
        );

        var recepcionReq = new RecepcionDteRequest(
            request.MhSettings.Environment, inv.SchemaVersion, inv.SchemaType, inv.GenerationCode, firmarDocumentoRes.ToJson()
        );

        await File.WriteAllTextAsync(
            request.ProcessingSettings.GetRecepcionRequestJsonPath(inv.ControlNumber), recepcionReq.ToJson(), cancellationToken
        );

        _fesvClient.Jwt = request.Jwt;

        var recepcionRes = await _fesvClient.RecepcionDteAsync(recepcionReq);

        await File.WriteAllTextAsync(
            request.ProcessingSettings.GetRecepcionResponseJsonPath(inv.ControlNumber), recepcionRes.ToJson(), cancellationToken
        );

        var result = false;

        if (recepcionRes.IsSuccessful)
        {
            dbContext.InvoiceProcessingLog.Add(
                InvoiceProcessingLog.CreateResponse(inv.Id, InvoiceProcessingStatus.Processed, recepcionReq.ToJson())
            );

            inv.ProcessingStatusId = (short)InvoiceProcessingStatus.Processed;
            inv.ProcessingDateTime = DateTime.Now;
            inv.ReceiptStamp = recepcionRes.SelloRecibido;
            inv.ExternalUrl = GetExternalUrl(inv.Pos.Branch.Company.Environment, inv.GenerationCode, inv.InvoiceDate);

            dbContext.InvoiceProcessingStatusLog.Add(new(inv.Id, inv.ProcessingStatusId));

            var received = FeFcv1Received.DeserializeReceived(inv.Serialization);

            received.SelloRecibido = inv.ReceiptStamp;
            received.FirmaElectronica = firmarDocumentoRes.Body;

            inv.Serialization = received.ToJson();

            result = true;
        }
        else
        {
            dbContext.InvoiceProcessingLog.Add(
                InvoiceProcessingLog.CreateResponse(inv.Id, InvoiceProcessingStatus.Declined, recepcionReq.ToJson())
            );

            inv.ProcessingStatusId = (short)InvoiceProcessingStatus.Declined;
            inv.SyncAttempts += 1;

            dbContext.InvoiceProcessingStatusLog.Add(new(inv.Id, inv.ProcessingStatusId));
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return result;
    }
}
