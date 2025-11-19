namespace InvoizR.Clients.DataContracts.Invoices;

public record InvoiceNotificationItemModel
{
    public string Email { get; set; }
    public bool? Bcc { get; set; }
    public short? Files { get; set; }
    public bool? Successful { get; set; }
    public DateTime? CreatedAt { get; set; }
}
