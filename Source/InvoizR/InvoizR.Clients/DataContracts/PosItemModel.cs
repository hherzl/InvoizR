namespace InvoizR.Clients.DataContracts;

public record PosItemModel
{
    public short? Id { get; set; }
    public short? BranchId { get; set; }
    public string Branch { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string TaxAuthPos { get; set; }
    public string Description { get; set; }
}
