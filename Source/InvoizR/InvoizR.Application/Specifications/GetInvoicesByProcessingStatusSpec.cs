using InvoizR.Clients.DataContracts;
using InvoizR.Domain.Common;

namespace InvoizR.Application.Specifications;

public class GetInvoicesByProcessingStatusSpec : BaseSpecification<InvoiceItemModel>
{
    public GetInvoicesByProcessingStatusSpec(short? processingStatusId)
    {
        if (processingStatusId != null)
            Criteria = item => item.ProcessingStatusId == processingStatusId;
    }
}
