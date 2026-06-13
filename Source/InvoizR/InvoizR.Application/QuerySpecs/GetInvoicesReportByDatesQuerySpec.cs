using InvoizR.Clients.DataContracts.Reports;
using InvoizR.Domain.Common;

namespace InvoizR.Application.QuerySpecs;

internal class GetInvoicesReportByDatesQuerySpec : BaseQuerySpec<InvoiceItemReportModel>
{
    public GetInvoicesReportByDatesQuerySpec(DateTime? startDate, DateTime? endDate)
    {
        if (startDate != null)
            Criteria = entity => entity.InvoiceDate >= startDate;

        if (endDate != null)
            Criteria = entity => entity.InvoiceDate <= endDate;
    }
}
