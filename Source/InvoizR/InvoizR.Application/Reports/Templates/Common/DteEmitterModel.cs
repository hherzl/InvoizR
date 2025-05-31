namespace InvoizR.Application.Reports.Templates.Common;

public record DteEmitterModel
{
    public string BusinessName { get; set; }
    public string TaxIdNumber { get; set; }
    public string TaxRegistrationNumber { get; set; }
    public string EconomicActivityId { get; set; }
    public string EconomicActivity { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Logo { get; set; }
}
