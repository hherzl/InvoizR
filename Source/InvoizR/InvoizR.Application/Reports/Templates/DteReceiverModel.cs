namespace InvoizR.Application.Reports.Templates;

public record DteReceiverModel
{
    public string WtId { get; set; }
    public string Name { get; set; }
    public string DocumentTypeId { get; set; }
    public string DocumentNumber { get; set; }
    public string CountryId { get; set; }
    public short? CountryLevelId { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}
