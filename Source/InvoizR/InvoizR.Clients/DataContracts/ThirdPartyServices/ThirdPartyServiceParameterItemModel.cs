namespace InvoizR.Clients.DataContracts.ThirdPartyServices;

public record ThirdPartyServiceParameterItemModel
{
    public short? Id { get; set; }
    public short? ThirdPartyServiceId { get; set; }
    public string Category { get; set; }
    public string Name { get; set; }
    public string DefaultValue { get; set; }
    public bool? RequiresEncryption { get; set; }
}
