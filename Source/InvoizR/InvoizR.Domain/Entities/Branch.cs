using System.Collections.ObjectModel;
using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class Branch : AuditableEntity
{
    public Branch()
    {
    }

    public Branch(short? id)
    {
        Id = id;
    }

    public short? Id { get; set; }
    public short? CompanyId { get; set; }
    public string Name { get; set; }
    public string TaxAuthId { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public byte[] Logo { get; set; }
    public int? Headquarters { get; set; }
    public short? ResponsibleId { get; set; }
    public string NonCustomerEmail { get; set; }

    public virtual Company Company { get; set; }
    public virtual Responsible Responsible { get; set; }
    public virtual Collection<Pos> Pos { get; set; }
    public virtual Collection<BranchNotification> BranchNotifications { get; set; }
}
