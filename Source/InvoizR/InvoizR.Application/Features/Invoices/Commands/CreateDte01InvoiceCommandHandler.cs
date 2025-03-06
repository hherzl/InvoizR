using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Features.Invoices.Commands;

public class CreateDte01InvoiceCommandHandler : IRequestHandler<CreateDte01InvoiceCommand, CreatedResponse<long?>>
{
    private readonly ILogger _logger;
    private readonly IInvoizRDbContext _dbContext;

    public CreateDte01InvoiceCommandHandler(ILogger<CreateDte01InvoiceCommandHandler> logger, IInvoizRDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<CreatedResponse<long?>> Handle(CreateDte01InvoiceCommand request, CancellationToken cancellationToken)
    {
        _ = await _dbContext.GetCurrentInvoiceTypeAsync(request.InvoiceTypeId, ct: cancellationToken) ?? throw new InvalidCurrentInvoiceTypeException();

        var txn = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var pos = await _dbContext.GetPosAsync(request.PosId, ct: cancellationToken);

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
                CreatedAt = DateTime.Now,
                InvoiceTypeId = request.InvoiceTypeId,
                InvoiceNumber = request.InvoiceNumber,
                InvoiceDate = request.InvoiceDate,
                InvoiceTotal = request.InvoiceTotal,
                Lines = request.Lines,
                Serialization = request.Dte.ToJson(),
                ProcessingStatusId = (short)InvoiceProcessingStatus.Created,
                RetryIn = 0,
                SyncAttempts = 0
            };

            _dbContext.Invoice.Add(invoice);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));

            await _dbContext.SaveChangesAsync(cancellationToken);

            await txn.CommitAsync(cancellationToken);

            return new(invoice.Id);
        }
        catch (Exception ex)
        {
            await txn.RollbackAsync(cancellationToken);

            _logger.LogCritical(ex, "There was an error on Create Dte01 Invoice");

            return new();
        }
    }
}
