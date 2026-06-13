using InvoizR.Clients.DataContracts.Reports;
using InvoizR.Domain.Common;

namespace InvoizR.Application.QuerySpecs;

internal class GetInvoicesReportByBranchQuerySpec : BaseQuerySpec<InvoiceItemReportModel>
{
    public GetInvoicesReportByBranchQuerySpec(short? branchId)
    {
        if (branchId != null)
            Criteria = entity => entity.BranchId == branchId;
    }
}
