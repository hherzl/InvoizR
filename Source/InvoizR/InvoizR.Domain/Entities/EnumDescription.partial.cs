namespace InvoizR.Domain.Entities;

public partial class EnumDescription
{
    public EnumDescription(int? value, string desc, string fullName)
    {
        Value = value;
        Desc = desc;
        FullName = fullName;
    }
}
