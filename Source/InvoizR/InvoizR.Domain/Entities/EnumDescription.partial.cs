namespace InvoizR.Domain.Entities;

public partial class EnumDescription
{
    public EnumDescription(int? id, string desc, string fullNamen)
    {
        Id = id;
        Desc = desc;
        FullName = fullNamen;
    }
}
