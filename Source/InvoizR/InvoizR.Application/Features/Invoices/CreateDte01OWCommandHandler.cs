using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Services;
using InvoizR.Clients.DataContracts.Dte01;
using InvoizR.Clients.DataContracts.Invoices;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using InvoizR.Domain.Notifications;
using InvoizR.SharedKernel.Mh;
using InvoizR.SharedKernel.Mh.FeFc;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Features.Invoices;

public sealed class CreateDte01OWCommandHandler(ILogger<CreateDte01OWCommandHandler> logger, IServiceProvider serviceProvider)
    : IRequestHandler<CreateDte01OWCommand, CreatedInvoiceResponse>
{
    public async Task<CreatedInvoiceResponse> Handle(CreateDte01OWCommand request, CancellationToken ct)
    {
        using var serviceScope = serviceProvider.CreateScope();

        using var dbContext = serviceScope.ServiceProvider.GetRequiredService<IInvoizRDbContext>();
        var dteSyncStatusChanger = serviceScope.ServiceProvider.GetRequiredService<Dte01SyncStatusChanger>();
        var seguridadClient = serviceScope.ServiceProvider.GetRequiredService<ISeguridadClient>();

        _ = await dbContext.GetCurrentInvoiceTypeAsync(FeFcv1.TypeId, ct: ct) ?? throw new InvalidCurrentInvoiceTypeException();

        var txn = await dbContext.Database.BeginTransactionAsync(ct);

        try
        {
            var pos = await dbContext.GetPosAsync(request.PosId, includes: true, ct: ct);

            var invoice = new Invoice
            {
                PosId = pos.Id,
                CustomerId = request.Customer.Id,
                CustomerDocumentTypeId = request.Customer.DocumentTypeId,
                CustomerDocumentNumber = request.Customer.DocumentNumber,
                CustomerWtId = request.Customer.WtId,
                CustomerName = request.Customer.Name,
                CustomerCountryId = request.Customer.CountryId,
                CustomerCountryLevelId = request.Customer.CountryLevelId,
                CustomerAddress = request.Customer.Address,
                CustomerPhone = request.Customer.Phone,
                CustomerEmail = request.Customer.Email,
                CustomerLastUpdated = DateTime.Now,
                InvoiceTypeId = FeFcv1.TypeId,
                InvoiceNumber = request.InvoiceNumber,
                InvoiceDate = request.InvoiceDate,
                InvoiceTotal = request.InvoiceTotal,
                Lines = request.Lines,
                Payload = request.Dte.ToJson(),
                ProcessingTypeId = (short)InvoiceProcessingType.OneWay,
                SyncStatusId = (short)SyncStatus.Created
            };

            dbContext.Invoice.Add(invoice);

            await dbContext.SaveChangesAsync(ct);

            dbContext.InvoiceSyncStatusLog.Add(new(invoice.Id, invoice.SyncStatusId));

            await dbContext.SaveChangesAsync(ct);

            await txn.CommitAsync(ct);

            var fallback = await dbContext.GetCurrentFallbackAsync(pos.Branch.Company.Id, ct: ct);
            if (fallback?.Enable == true)
            {
                logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{SyncStatus.Created}'...");

                await dteSyncStatusChanger.SetInvoiceAsInitializedAsync(invoice.Id, dbContext, ct);

                var dte = FeFcv1.Deserialize(invoice.Payload);
                dte.Identificacion.TipoModelo = MhCatalog.Cat003.Contingencia;
                dte.Identificacion.TipoOperacion = MhCatalog.Cat004.Contingencia;

                invoice.Payload = dte.ToJson();

                logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{SyncStatus.Initialized}'...");

                invoice.FallbackId = fallback.Id;

                await dteSyncStatusChanger.SetInvoiceAsFallbackAsync(invoice.Id, dbContext, ct);

                invoice.AddNotification(new ExportFallbackInvoiceNotification(invoice));
                await dbContext.DispatchNotificationsAsync(ct);
            }

            return invoice.ToCreateResponse();
        }
        catch (Exception ex)
        {
            await txn.RollbackAsync(ct);
            logger.LogCritical(ex, "There was an error on Create DTE-01 in OW processing");
            return new();
        }
    }
}
