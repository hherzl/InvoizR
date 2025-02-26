using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class EnumDescription : Entity
{
    public EnumDescription()
    {
    }

    public EnumDescription(int? id)
    {
        Id = id;
    }

    public int? Id { get; set; }
    public string Desc { get; set; }
    public string FullName { get; set; }
}
