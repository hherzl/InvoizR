using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Dte03;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using InvoizR.SharedKernel.Mh.FeCcf;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Features.Invoices.Commands;

public sealed class CreateDte03OWCommandHandler : IRequestHandler<CreateDte03OWCommand, CreatedResponse<long?>>
{
    private readonly ILogger _logger;
    private readonly IInvoizRDbContext _dbContext;

    public CreateDte03OWCommandHandler(ILogger<CreateDte03OWCommandHandler> logger, IInvoizRDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<CreatedResponse<long?>> Handle(CreateDte03OWCommand request, CancellationToken cancellationToken)
    {
        _ = await _dbContext.GetCurrentInvoiceTypeAsync(FeCcfv3.TypeId, ct: cancellationToken) ?? throw new InvalidCurrentInvoiceTypeException();

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
                InvoiceTypeId = FeCcfv3.TypeId,
                InvoiceNumber = request.InvoiceNumber,
                InvoiceDate = request.InvoiceDate,
                InvoiceTotal = request.InvoiceTotal,
                Lines = request.Lines,
                Payload = request.Dte.ToJson(),
                ProcessingTypeId = (short)InvoiceProcessingType.OneWay,
                ProcessingStatusId = (short)InvoiceProcessingStatus.Created
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
            _logger.LogCritical(ex, "There was an error on Create DTE-03 in OW processing");
            return new();
        }
    }
}
