using System.Collections.ObjectModel;
using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

public partial class InvoiceType : Entity
{
    public InvoiceType()
    {
    }

    public InvoiceType(short? id)
    {
        Id = id;
    }

    public short? Id { get; set; }
    public string Name { get; set; }
    public string SchemaType { get; set; }
    public short? SchemaVersion { get; set; }
    public bool? Current { get; set; }

    public virtual Collection<BranchNotification> BranchNotifications { get; set; }
}
