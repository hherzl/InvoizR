﻿using InvoizR.Application.Common.Persistence;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Domain.Entities;
using MediatR;

namespace InvoizR.Application.Features.Branches.Commands;

public class AddNotificationToBranchCommandHandler : IRequestHandler<AddNotificationToBranchCommand, CreatedResponse<short?>>
{
    private readonly IInvoizRDbContext _dbContext;

    public AddNotificationToBranchCommandHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreatedResponse<short?>> Handle(AddNotificationToBranchCommand request, CancellationToken cancellationToken)
    {
        var entity = new BranchNotification
        {
            BranchId = request.BranchId,
            InvoiceTypeId = request.InvoiceTypeId,
            Email = request.Email,
            Bcc = request.Bcc
        };

        _dbContext.BranchNotification.Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(entity.Id);
    }
}
