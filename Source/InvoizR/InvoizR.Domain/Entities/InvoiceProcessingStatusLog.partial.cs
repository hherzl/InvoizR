namespace InvoizR.Domain.Entities;

public partial class InvoiceProcessingStatusLog
{
    public InvoiceProcessingStatusLog(long? invoiceId, short? processingStatusId)
    {
        InvoiceId = invoiceId;
        CreatedAt = DateTime.Now;
        ProcessingStatusId = processingStatusId;
    }
}
