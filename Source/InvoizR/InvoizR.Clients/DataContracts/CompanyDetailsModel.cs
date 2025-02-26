namespace InvoizR.Clients.DataContracts;

public record CompanyDetailsModel
{
    public CompanyDetailsModel()
    {
        Branches = [];
    }

    public short? Id { get; set; }
    public string Environment { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string BusinessName { get; set; }
    public string TaxIdNumber { get; set; }
    public string TaxRegistrationNumber { get; set; }
    public string EconomicActivityId { get; set; }
    public string EconomicActivity { get; set; }
    public short? CountryLevelId { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Logo { get; set; }
    public int? Headquarters { get; set; }

    public List<BranchItemModel> Branches { get; set; }
}
