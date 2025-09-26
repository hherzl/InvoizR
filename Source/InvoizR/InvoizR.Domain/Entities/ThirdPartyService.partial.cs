namespace InvoizR.Domain.Entities;

public partial class ThirdPartyService
{
    public ThirdPartyService(string environmentId, string name, string description)
    {
        EnvironmentId = environmentId;
        Name = name;
        Description = description;
    }
}
