using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts.Reports;

public record GetInvoicesReportQuery : IRequest<ListResponse<InvoiceItemReportModel>>
{
    public GetInvoicesReportQuery()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public short? BranchId { get; set; }
    public short? PosId { get; set; }
    public string CustomerName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public short? InvoiceTypeId { get; set; }
    public short? ProcessingTypeId { get; set; }
    public short? SyncStatusId { get; set; }
}
