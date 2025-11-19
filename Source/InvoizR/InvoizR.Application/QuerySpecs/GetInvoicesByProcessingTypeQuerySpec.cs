using InvoizR.Clients.DataContracts.Invoices;
using InvoizR.Domain.Common;

namespace InvoizR.Application.QuerySpecs;

public class GetInvoicesByProcessingTypeQuerySpec : BaseQuerySpec<InvoiceItemModel>
{
    public GetInvoicesByProcessingTypeQuerySpec(short? processingTypeId)
    {
        if (processingTypeId != null)
            Criteria = entity => entity.ProcessingTypeId == processingTypeId;
    }
}
