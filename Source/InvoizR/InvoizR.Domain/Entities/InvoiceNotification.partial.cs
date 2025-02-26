namespace InvoizR.Domain.Entities;

public partial class InvoiceNotification
{
    public InvoiceNotification(long? invoiceId, string email, bool? bcc, short files, bool? successful)
    {
        InvoiceId = invoiceId;
        Email = email;
        Bcc = bcc;
        Files = files;
        Successful = successful;

        CreatedAt = DateTime.Now;
    }
}
