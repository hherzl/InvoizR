using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Invoices.Queries;

public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, InvoiceDetailsModel>
{
    private readonly IInvoizRDbContext _dbContext;

    public GetInvoiceQueryHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<InvoiceDetailsModel> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.GetInvoiceAsync(request.Id, ct: cancellationToken);
        if (entity == null)
            return null;

        var processingStatuses = await _dbContext.VInvoiceProcessingStatus.ToListAsync(cancellationToken);

        var processingStatusLogs = await _dbContext.GetInvoiceProcessingStatusLogs(entity.Id).ToListAsync(cancellationToken);
        var processingLogs = await _dbContext.GetInvoiceProcessingLogs(entity.Id).ToListAsync(cancellationToken);
        var files = await _dbContext.GetInvoiceFiles(entity.Id).ToListAsync(cancellationToken);
        var notifications = await _dbContext.GetInvoiceNotifications(entity.Id).ToListAsync(cancellationToken);

        var model = new InvoiceDetailsModel
        {
            Id = entity.Id,
            FallbackId = entity.FallbackId,
            PosId = entity.PosId,
            CustomerId = entity.CustomerId,
            CustomerWtId = entity.CustomerWtId,
            CustomerName = entity.CustomerName,
            CustomerCountryId = entity.CustomerCountryId,
            CustomerCountryLevelId = entity.CustomerCountryLevelId,
            CustomerAddress = entity.CustomerAddress,
            CustomerPhone = entity.CustomerPhone,
            CustomerEmail = entity.CustomerEmail,
            CustomerLastUpdated = entity.CustomerLastUpdated,
            CreatedAt = entity.CreatedAt,
            InvoiceTypeId = entity.InvoiceTypeId,
            InvoiceNumber = entity.InvoiceNumber,
            InvoiceDate = entity.InvoiceDate,
            InvoiceTotal = entity.InvoiceTotal,
            Lines = entity.Lines,
            SchemaType = entity.SchemaType,
            SchemaVersion = entity.SchemaVersion,
            GenerationCode = entity.GenerationCode,
            ControlNumber = entity.ControlNumber,
            Payload = entity.Payload,
            ProcessingStatusId = entity.ProcessingStatusId,
            ProcessingStatus = processingStatuses.First(item => item.Id == entity.ProcessingStatusId).Desc,
            RetryIn = entity.RetryIn,
            SyncAttempts = entity.SyncAttempts,
            EmitDateTime = entity.EmitDateTime,
            ReceiptStamp = entity.ReceiptStamp,
            ExternalUrl = entity.ExternalUrl,
            Notes = entity.Notes,
            Processed = entity.ProcessingStatusId >= (short)InvoiceProcessingStatus.Processed,
            ProcessingStatusLogs = processingStatusLogs,
            ProcessingLogs = processingLogs,
            Files = files,
            Notifications = notifications
        };

        return model;
    }
}
