using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Services;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Dte01;
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

namespace InvoizR.Application.Features.Invoices.Commands;

public sealed class CreateDte01OWCommandHandler(ILogger<CreateDte01OWCommandHandler> logger, IServiceProvider serviceProvider)
    : IRequestHandler<CreateDte01OWCommand, CreatedResponse<long?>>
{
    public async Task<CreatedResponse<long?>> Handle(CreateDte01OWCommand request, CancellationToken st)
    {
        using var scope = serviceProvider.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<IInvoizRDbContext>();
        var dteSyncStatusChanger = scope.ServiceProvider.GetRequiredService<Dte01SyncStatusChanger>();
        var seguridadClient = scope.ServiceProvider.GetRequiredService<ISeguridadClient>();

        _ = await dbContext.GetCurrentInvoiceTypeAsync(FeFcv1.TypeId, ct: st) ?? throw new InvalidCurrentInvoiceTypeException();

        var txn = await dbContext.Database.BeginTransactionAsync(st);

        try
        {
            var pos = await dbContext.GetPosAsync(request.PosId, includes: true, ct: st);

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
                ProcessingStatusId = (short)InvoiceProcessingStatus.Created
            };

            dbContext.Invoice.Add(invoice);

            await dbContext.SaveChangesAsync(st);

            dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));

            await dbContext.SaveChangesAsync(st);

            await txn.CommitAsync(st);

            var fallback = await dbContext.GetCurrentFallbackAsync(pos.Branch.Company.Id, ct: st);
            if (fallback?.Enable == true)
            {
                logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{InvoiceProcessingStatus.Created}'...");

                await dteSyncStatusChanger.SetInvoiceAsInitializedAsync(invoice.Id, dbContext, st);

                var dte = FeFcv1.Deserialize(invoice.Payload);
                dte.Identificacion.TipoModelo = MhCatalog.Cat003.Contingencia;
                dte.Identificacion.TipoOperacion = MhCatalog.Cat004.Contingencia;

                invoice.Payload = dte.ToJson();

                logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{InvoiceProcessingStatus.Initialized}'...");

                invoice.FallbackId = fallback.Id;

                await dteSyncStatusChanger.SetInvoiceAsFallbackAsync(invoice.Id, dbContext, st);

                invoice.AddNotification(new ExportFallbackInvoiceNotification(invoice));
                await dbContext.DispatchNotificationsAsync(st);
            }

            return new(invoice.Id);
        }
        catch (Exception ex)
        {
            await txn.RollbackAsync(st);
            logger.LogCritical(ex, "There was an error on Create DTE-01 in OW processing");
            return new();
        }
    }
}
