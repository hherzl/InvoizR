using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts.Dte03;
using InvoizR.Clients.DataContracts.Invoices;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using InvoizR.SharedKernel.Mh.FeCcf;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Features.Invoices;

public sealed class CreateDte03OWCommandHandler(ILogger<CreateDte03OWCommandHandler> logger, IServiceProvider serviceProvider)
    : IRequestHandler<CreateDte03OWCommand, CreatedInvoiceResponse>
{
    public async Task<CreatedInvoiceResponse> Handle(CreateDte03OWCommand request, CancellationToken ct = default)
    {
        using var serviceScope = serviceProvider.CreateScope();

        using var dbContext = serviceScope.ServiceProvider.GetRequiredService<IInvoizRDbContext>();
        _ = await dbContext.GetCurrentInvoiceTypeAsync(FeCcfv3.TypeId, ct: ct) ?? throw new InvalidCurrentInvoiceTypeException();

        var txn = await dbContext.Database.BeginTransactionAsync(ct);

        try
        {
            var pos = await dbContext.GetPosAsync(request.PosId, ct: ct);

            var invoice = new Invoice
            {
                PosId = pos.Id,
                CustomerId = request.Customer.Id,
                CustomerDocumentTypeId = request.Customer.DocumentTypeId,
                CustomerDocumentNumber = request.Customer.DocumentNumber,
                CustomerTaxpayerRegistrationNumber = request.Customer.TaxpayerRegistrationNumber,
                CustomerWtId = request.Customer.WtId,
                CustomerName = request.Customer.Name,
                CustomerCountryId = request.Customer.CountryId,
                CustomerCountryLevelId = request.Customer.CountryLevelId,
                CustomerAddress = request.Customer.Address,
                CustomerPhone = request.Customer.Phone,
                CustomerEmail = request.Customer.Email,
                CustomerLastUpdated = DateTime.Now,
                InvoiceTypeId = FeCcfv3.TypeId,
                InvoiceNumber = request.InvoiceNumber,
                InvoiceDate = request.InvoiceDate,
                InvoiceTotal = request.InvoiceTotal,
                Lines = request.Lines,
                Payload = request.Dte.ToJson(),
                ProcessingTypeId = (short)InvoiceProcessingType.OneWay,
                SyncStatusId = (short)SyncStatus.Created
            };

            dbContext.Invoices.Add(invoice);

            await dbContext.SaveChangesAsync(ct);

            dbContext.InvoiceSyncStatusLogs.Add(new(invoice.Id, invoice.SyncStatusId));

            await dbContext.SaveChangesAsync(ct);

            await txn.CommitAsync(ct);

            return invoice.ToCreateResponse();
        }
        catch (Exception ex)
        {
            await txn.RollbackAsync(ct);
            logger.LogCritical(ex, "There was an error on Create DTE-03 in OW processing");
            return new();
        }
    }
}
