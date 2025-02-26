﻿using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts;

public record GetInvoicesQuery : IRequest<PagedResponse<InvoiceItemModel>>
{
    public GetInvoicesQuery()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public short? PosId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string ControlNumber { get; set; }
    public short? ProcessingStatusId { get; set; }
}
