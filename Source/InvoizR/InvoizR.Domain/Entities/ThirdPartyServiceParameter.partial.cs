namespace InvoizR.Domain.Entities;

public partial class ThirdPartyServiceParameter
{
    public ThirdPartyServiceParameter(string category, string name, string defaultValue)
    {
        Category = category;
        Name = name;
        DefaultValue = defaultValue;
        RequiresEncryption = false;
    }
}
