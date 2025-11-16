using InvoizR.Domain.Entities;

namespace InvoizR.Application.Services.Models;

public record WebhookNotificationModel
{
    public WebhookNotificationModel()
    {
        Invoice = new();
    }

    public WebhookNotificationModel(Invoice invoice)
        : this()
    {
        WebhookNotificationProtocol = invoice.Pos.Branch.Company.WebhookNotificationProtocol;
        WebhookNotificationAddress = invoice.Pos.Branch.Company.WebhookNotificationAddress;
        WebhookNotificationMisc1 = invoice.Pos.Branch.Company.WebhookNotificationMisc1;
        WebhookNotificationMisc2 = invoice.Pos.Branch.Company.WebhookNotificationMisc2;

        Invoice.InvoiceId = invoice.Id;
        Invoice.CustomerId = invoice.CustomerId;
        Invoice.CustomerName = invoice.CustomerName;
        Invoice.InvoiceTypeId = invoice.InvoiceTypeId;
        Invoice.InvoiceNumber = invoice.InvoiceNumber;
        Invoice.InvoiceDate = invoice.InvoiceDate;
        Invoice.InvoiceTotal = invoice.InvoiceTotal;
        Invoice.InvoiceGuid = invoice.InvoiceGuid;
        Invoice.AuditNumber = invoice.AuditNumber;
        Invoice.ReceiptStamp = invoice.ReceiptStamp;
        Invoice.ExternalUrl = invoice.ExternalUrl;
    }

    public string WebhookNotificationProtocol { get; set; }
    public string WebhookNotificationAddress { get; set; }
    public string WebhookNotificationMisc1 { get; set; }
    public string WebhookNotificationMisc2 { get; set; }

    public bool IsHttp()
        => string.Compare(WebhookNotificationProtocol, "http", true) == 0 || string.Compare(WebhookNotificationProtocol, "https", true) == 0;

    public bool IsHttpPost()
        => IsHttp() && string.Compare(WebhookNotificationMisc1, "post", true) == 0;

    public bool IsHttpPut()
        => IsHttp() && string.Compare(WebhookNotificationMisc1, "put", true) == 0;

    public bool IsHttpPatch()
        => IsHttp() && string.Compare(WebhookNotificationMisc1, "patch", true) == 0;

    public WebhookInvoiceNodeModel Invoice { get; set; }
}
