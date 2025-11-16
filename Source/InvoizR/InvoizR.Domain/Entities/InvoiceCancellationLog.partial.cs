using InvoizR.Domain.Enums;
using InvoizR.Domain.Notifications;
using InvoizR.SharedKernel;

namespace InvoizR.Domain.Entities;

public partial class InvoiceCancellationLog
{
    public static InvoiceCancellationLog CreateRequest(long? invoiceId, string payload)
        => new()
        {
            InvoiceId = invoiceId,
            ProcessingStatusId = (short)InvoiceProcessingStatus.Requested,
            LogType = Tokens.Request,
            ContentType = Tokens.ApplicationJson,
            Payload = payload
        };

    public static InvoiceCancellationLog CreateResponse(long? invoiceId, string payload, InvoiceProcessingStatus processingStatus)
    {
        var entity = new InvoiceCancellationLog
        {
            InvoiceId = invoiceId,
            ProcessingStatusId = (short)processingStatus,
            LogType = Tokens.Response,
            ContentType = Tokens.ApplicationJson,
            Payload = payload
        };

        if (processingStatus == InvoiceProcessingStatus.Processed)
            entity.Notifications.Add(new CancelInvoiceNotification(invoiceId, payload));

        return entity;
    }
}
