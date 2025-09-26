namespace InvoizR.Domain.Entities;

public partial class ThirdPartyServiceParameter
{
    public ThirdPartyServiceParameter(string category, string name, string value)
    {
        Category = category;
        Name = name;
        Value = value;
        RequiresEncryption = false;
    }
}
