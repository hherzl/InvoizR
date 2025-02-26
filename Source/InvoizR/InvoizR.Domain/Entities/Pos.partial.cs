namespace InvoizR.Domain.Entities;

public partial class Pos
{
    public Pos(short? branchId, string name, string code, string description)
    {
        BranchId = branchId;
        Name = name;
        Code = code;
        Description = description;
    }
}
