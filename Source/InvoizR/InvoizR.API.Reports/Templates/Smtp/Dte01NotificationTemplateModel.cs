using InvoizR.Domain.Entities;

namespace InvoizR.API.Reports.Templates.Smtp;

public record Dte01NotificationTemplateModel
{
    public Dte01NotificationTemplateModel(Branch branch, InvoiceType invoiceType, Invoice invoice)
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
        ControlNumber = invoice.ControlNumber;

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
    public string ControlNumber { get; }

    public List<string> Copies { get; set; }
    public List<string> Bcc { get; }
}
