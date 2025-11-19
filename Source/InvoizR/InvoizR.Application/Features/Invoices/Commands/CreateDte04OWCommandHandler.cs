using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts.Dte04;
using InvoizR.Clients.DataContracts.Invoices;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using InvoizR.SharedKernel.Mh.FeNr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Features.Invoices.Commands;

public sealed class CreateDte04OWCommandHandler(ILogger<CreateDte04OWCommandHandler> logger, IInvoizRDbContext dbContext)
    : IRequestHandler<CreateDte04OWCommand, CreatedInvoiceResponse>
{
    public async Task<CreatedInvoiceResponse> Handle(CreateDte04OWCommand request, CancellationToken ct)
    {
        _ = await dbContext.GetCurrentInvoiceTypeAsync(FeNrv3.TypeId, ct: ct) ?? throw new InvalidCurrentInvoiceTypeException();

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
                InvoiceTypeId = FeNrv3.TypeId,
                InvoiceNumber = request.InvoiceNumber,
                InvoiceDate = request.InvoiceDate,
                InvoiceTotal = request.InvoiceTotal,
                Lines = request.Lines,
                Payload = request.Dte.ToJson(),
                ProcessingTypeId = (short)InvoiceProcessingType.OneWay,
                ProcessingStatusId = (short)InvoiceProcessingStatus.Created
            };

            dbContext.Invoice.Add(invoice);

            await dbContext.SaveChangesAsync(ct);

            dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));

            await dbContext.SaveChangesAsync(ct);

            await txn.CommitAsync(ct);

            return new(invoice.Id, invoice.InvoiceTypeId, invoice.SchemaType, invoice.SchemaVersion, invoice.InvoiceGuid, invoice.AuditNumber);
        }
        catch (Exception ex)
        {
            await txn.RollbackAsync(ct);
            logger.LogCritical(ex, "There was an error on Create DTE-04 in OW processing");
            return new();
        }
    }
}
