using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts.Dte14;
using InvoizR.Clients.DataContracts.Invoices;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using InvoizR.SharedKernel.Mh.FeFse;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Features.Invoices;

public sealed class CreateDte14OWCommandHandler(ILogger<CreateDte14OWCommandHandler> logger, IInvoizRDbContext dbContext)
    : IRequestHandler<CreateDte14OWCommand, CreatedInvoiceResponse>
{
    public async Task<CreatedInvoiceResponse> Handle(CreateDte14OWCommand request, CancellationToken ct = default)
    {
        _ = await dbContext.GetCurrentInvoiceTypeAsync(FeFsev1.TypeId, ct: ct) ?? throw new InvalidCurrentInvoiceTypeException();

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
                CustomerWtId = request.Customer.WtId,
                CustomerName = request.Customer.Name,
                CustomerCountryId = request.Customer.CountryId,
                CustomerCountryLevelId = request.Customer.CountryLevelId,
                CustomerAddress = request.Customer.Address,
                CustomerPhone = request.Customer.Phone,
                CustomerEmail = request.Customer.Email,
                CustomerLastUpdated = DateTime.Now,
                InvoiceTypeId = FeFsev1.TypeId,
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
            logger.LogCritical(ex, "There was an error on Create DTE-14 in OW processing");
            return new();
        }
    }
}
