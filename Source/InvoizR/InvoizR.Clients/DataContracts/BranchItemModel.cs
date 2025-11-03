namespace InvoizR.Clients.DataContracts;

public record BranchItemModel
{
    public short? Id { get; set; }
    public short? CompanyId { get; set; }
    public string Company { get; set; }
    public string Name { get; set; }
    public string TaxAuthId { get; set; }
}
