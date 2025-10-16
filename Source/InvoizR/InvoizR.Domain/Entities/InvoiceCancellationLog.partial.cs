using InvoizR.Domain.Enums;
using InvoizR.Domain.Notifications;

namespace InvoizR.Domain.Entities;

public partial class InvoiceCancellationLog
{
    public static InvoiceCancellationLog CreateRequest(long? invoiceId, string payload)
        => new()
        {
            InvoiceId = invoiceId,
            ProcessingStatusId = (short)InvoiceProcessingStatus.Requested,
            LogType = "REQUEST",
            ContentType = "application/json",
            Payload = payload
        };

    public static InvoiceCancellationLog CreateResponse(long? invoiceId, string payload, InvoiceProcessingStatus processingStatus)
    {
        var entity = new InvoiceCancellationLog
        {
            InvoiceId = invoiceId,
            ProcessingStatusId = (short)processingStatus,
            LogType = "RESPONSE",
            ContentType = "application/json",
            Payload = payload
        };

        if (processingStatus == InvoiceProcessingStatus.Processed)
            entity.Notifications.Add(new CancelInvoiceNotification(invoiceId, payload));

        return entity;
    }
}
