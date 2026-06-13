using InvoizR.Clients.DataContracts.Reports;
using InvoizR.Domain.Common;

namespace InvoizR.Application.QuerySpecs;

internal class GetInvoicesReportByInvoiceTypeQuerySpec : BaseQuerySpec<InvoiceItemReportModel>
{
    public GetInvoicesReportByInvoiceTypeQuerySpec(short? invoiceTypeId)
    {
        if (invoiceTypeId != null)
            Criteria = entity => entity.InvoiceTypeId == invoiceTypeId;
    }
}
