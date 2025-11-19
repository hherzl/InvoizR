namespace InvoizR.Clients.DataContracts.Invoices;

public record InvoiceTypeItemModel
{
    public short? Id { get; set; }
    public string Name { get; set; }
    public string SchemaType { get; set; }
    public short? SchemaVersion { get; set; }
    public bool? Current { get; set; }
}
