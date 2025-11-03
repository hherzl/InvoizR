using InvoizR.Domain.Entities;

namespace InvoizR.Application.Reports.Templates.Common;

public record DteNotificationTemplateModel
{
    public DteNotificationTemplateModel(Branch branch, InvoiceType invoiceType, Invoice invoice)
    {
        Title = "Documento Tributario Electrónico";
        SourceEmail = branch.Email;

        Branch = branch.Name;
        BranchPhone = branch.Phone;

        InvoiceType = invoiceType.Name;

        CustomerName = invoice.CustomerName;
        CustomerEmail = invoice.CustomerEmail;
        InvoiceDate = invoice.InvoiceDate;
        InvoiceTotal = invoice.InvoiceTotal;
        AuditNumber = invoice.AuditNumber;

        Copies = [];
        Bcc = [];
    }

    public string Title { get; }
    public string SourceEmail { get; }

    public string Branch { get; }
    public string BranchPhone { get; }

    public string InvoiceType { get; }

    public string CustomerName { get; }
    public string CustomerEmail { get; }
    public DateTime? InvoiceDate { get; }
    public decimal? InvoiceTotal { get; }
    public string AuditNumber { get; }

    public List<string> Copies { get; }
    public List<string> Bcc { get; }
}
