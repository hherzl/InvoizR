using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Helpers;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Services;

public abstract class DteProcessingService
{
    private readonly ILogger _logger;

    public DteProcessingService(ILogger logger)
    {
        _logger = logger;
    }

    protected abstract bool Init(Invoice invoice);

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

        if (Init(inv))
            inv.ProcessingStatusId = (short)InvoiceProcessingStatus.Requested;
        else
            inv.ProcessingStatusId = (short)InvoiceProcessingStatus.InvalidSchema;

        dbContext.InvoiceProcessingStatusLog.Add(new(inv.Id, inv.ProcessingStatusId));

        _logger.LogInformation($" Updating '{inv.InvoiceNumber}' invoice: '{inv.SchemaType}{inv.SchemaVersion}', '{inv.GenerationCode}', '{inv.ControlNumber}'...");

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
