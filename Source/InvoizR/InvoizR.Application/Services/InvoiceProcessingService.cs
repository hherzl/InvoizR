using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Helpers;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.SharedKernel.Mh;
using InvoizR.SharedKernel.Mh.FeFc;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Services;

public class InvoiceProcessingService
{
    private readonly ILogger _logger;

    public InvoiceProcessingService(ILogger<InvoiceProcessingService> logger)
    {
        _logger = logger;
    }

    private bool TryInit(Invoice invoice)
    {
        FeFcv1 dte;

        try
        {
            dte = FeFcv1.Deserialize(invoice.Serialization);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, $"There was an error deserializing {invoice.InvoiceNumber} invoice");
            return false;
        }

        dte.Identificacion.Version = (int)invoice.SchemaVersion;
        dte.Identificacion.TipoDte = invoice.SchemaType;
        dte.Identificacion.CodigoGeneracion = invoice.GenerationCode;
        dte.Identificacion.NumeroControl = invoice.ControlNumber;

        dte.Resumen.TotalLetras = MoneyToWordsConverter.SpellingNumber(invoice.InvoiceTotal);

        invoice.Serialization = dte.ToJson();

        return true;
    }

    public async Task SetInvoiceAsInitializedAsync(long? invoiceId, IInvoizRDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var inv = await dbContext.GetInvoiceAsync(invoiceId, includes: true, tracking: true, ct: cancellationToken);

        var invType = await dbContext.GetInvoiceTypeAsync(inv.InvoiceTypeId, ct: cancellationToken);

        var dteInfo = DteInfoHelper.Get(invType.Id, inv.Pos.Branch.EstablishmentPrefix, inv.Pos.Branch.TaxAuthId, inv.Pos.Code, inv.InvoiceNumber);

        inv.SchemaType = invType.SchemaType;
        inv.SchemaVersion = invType.SchemaVersion;
        inv.GenerationCode = dteInfo.GenerationCode;
        inv.ControlNumber = dteInfo.ControlNumber;
        inv.ProcessingStatusId = (short)InvoiceProcessingStatus.Initialized;

        dbContext.InvoiceProcessingStatusLog.Add(new(inv.Id, inv.ProcessingStatusId));

        _logger.LogInformation($" Updating '{inv.InvoiceNumber}' invoice: '{inv.SchemaType}{inv.SchemaVersion}', '{inv.GenerationCode}', '{inv.ControlNumber}'...");

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SetInvoiceAsRequestedAsync(long? invoiceId, IInvoizRDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var inv = await dbContext.GetInvoiceAsync(invoiceId, tracking: true, ct: cancellationToken);

        if (TryInit(inv))
            inv.ProcessingStatusId = (short)InvoiceProcessingStatus.Requested;
        else
            inv.ProcessingStatusId = (short)InvoiceProcessingStatus.InvalidSchema;

        dbContext.InvoiceProcessingStatusLog.Add(new(inv.Id, inv.ProcessingStatusId));

        _logger.LogInformation($" Updating '{inv.InvoiceNumber}' invoice: '{inv.SchemaType}{inv.SchemaVersion}', '{inv.GenerationCode}', '{inv.ControlNumber}'...");

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
