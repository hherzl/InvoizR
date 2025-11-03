using InvoizR.Application.Common.Contracts;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Services;

public abstract class InvoiceSyncStatusChanger(ILogger logger)
{
    protected abstract bool Init(Invoice invoice);

    public async Task SetInvoiceAsInitializedAsync(long? invoiceId, IInvoizRDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var invoice = await dbContext.GetInvoiceAsync(invoiceId, includes: true, tracking: true, ct: cancellationToken);
        var invoiceType = await dbContext.GetInvoiceTypeAsync(invoice.InvoiceTypeId, ct: cancellationToken);

        var invoiceCodes = new InvoiceCodeGenerator().Generate(invoiceType.Id, invoice.Pos.Branch.TaxAuthId, invoice.Pos.TaxAuthId, invoice.InvoiceNumber);

        invoice.SchemaType = invoiceType.SchemaType;
        invoice.SchemaVersion = invoiceType.SchemaVersion;
        invoice.InvoiceGuid = invoiceCodes.InvoiceGuid;
        invoice.AuditNumber = invoiceCodes.AuditNumber;
        invoice.ProcessingStatusId = (short)InvoiceProcessingStatus.Initialized;

        dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));

        logger.LogInformation($" Updating '{invoice.InvoiceNumber}' invoice: '{invoice.SchemaType}{invoice.SchemaVersion}', '{invoice.InvoiceGuid}', '{invoice.AuditNumber}'...");

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SetInvoiceAsFallbackAsync(long? invoiceId, IInvoizRDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var invoice = await dbContext.GetInvoiceAsync(invoiceId, tracking: true, ct: cancellationToken);

        invoice.ProcessingTypeId = (short)InvoiceProcessingType.OneWay;
        invoice.ProcessingStatusId = (short)InvoiceProcessingStatus.Fallback;

        dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));

        logger.LogInformation($" Updating '{invoice.InvoiceNumber}' invoice: '{invoice.SchemaType}{invoice.SchemaVersion}', '{invoice.InvoiceGuid}', '{invoice.AuditNumber}'...");

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SetInvoiceAsRequestedAsync(long? invoiceId, IInvoizRDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var invoice = await dbContext.GetInvoiceAsync(invoiceId, tracking: true, ct: cancellationToken);

        if (Init(invoice))
            invoice.ProcessingStatusId = (short)InvoiceProcessingStatus.Requested;
        else
            invoice.ProcessingStatusId = (short)InvoiceProcessingStatus.InvalidSchema;

        dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));

        logger.LogInformation($" Updating '{invoice.InvoiceNumber}' invoice: '{invoice.SchemaType}{invoice.SchemaVersion}', '{invoice.InvoiceGuid}', '{invoice.AuditNumber}'...");

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
