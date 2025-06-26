namespace InvoizR.Clients.DataContracts;

public record BranchDetailsModel
{
    public BranchDetailsModel()
    {
        Pos = [];
    }

    public short? Id { get; set; }
    public short? CompanyId { get; set; }
    public string Name { get; set; }
    public string EstablishmentPrefix { get; set; }
    public string TaxAuthId { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Logo { get; set; }
    public int? Headquarters { get; set; }
    public short? ResponsibleId { get; set; }

    public List<PosItemModel> Pos { get; set; }

    public bool HasLogo
        => !string.IsNullOrEmpty(Logo);
}
