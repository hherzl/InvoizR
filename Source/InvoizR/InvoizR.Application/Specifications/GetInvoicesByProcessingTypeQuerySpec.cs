using InvoizR.Clients.DataContracts;
using InvoizR.Domain.Common;

namespace InvoizR.Application.Specifications;

public class GetInvoicesByProcessingTypeQuerySpec : BaseSpecification<InvoiceItemModel>
{
    public GetInvoicesByProcessingTypeQuerySpec(short? processingTypeId)
    {
        if (processingTypeId != null)
            Criteria = item => item.ProcessingTypeId == processingTypeId;
    }
}
