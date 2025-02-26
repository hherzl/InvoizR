using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class BranchNotification : Entity
{
    public BranchNotification()
    {
    }

    public BranchNotification(short? id)
    {
        Id = id;
    }

    public short? Id { get; set; }
    public short? BranchId { get; set; }
    public short? InvoiceTypeId { get; set; }
    public string Email { get; set; }
    public bool? Bcc { get; set; }

    public virtual Branch Branch { get; set; }
    public virtual InvoiceType InvoiceType { get; set; }
}
