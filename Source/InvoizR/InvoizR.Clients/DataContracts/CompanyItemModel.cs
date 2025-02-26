namespace InvoizR.Clients.DataContracts;

public record CompanyItemModel
{
    public short? Id { get; set; }
    public string Environment { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string BusinessName { get; set; }
    public string TaxIdNumber { get; set; }
    public string TaxRegistrationNumber { get; set; }
}
