using InvoizR.Clients.DataContracts.Reports;
using InvoizR.Domain.Common;

namespace InvoizR.Application.QuerySpecs;

internal class GetInvoicesReportByPosQuerySpec : BaseQuerySpec<InvoiceItemReportModel>
{
    public GetInvoicesReportByPosQuerySpec(short? posId)
    {
        if (posId != null)
            Criteria = entity => entity.PosId == posId;
    }
}
