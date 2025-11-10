using System.Diagnostics;
using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

[DebuggerDisplay("Category={Category}, Name={Name}, Value={Value}, RequiresEncryption={RequiresEncryption}")]
public partial class ThirdPartyServiceParameter : AuditableEntity
{
    public ThirdPartyServiceParameter()
    {
    }

    public ThirdPartyServiceParameter(short? id)
    {
        Id = id;
    }

    public short? Id { get; set; }
    public short? ThirdPartyServiceId { get; set; }
    public string Category { get; set; }
    public string Name { get; set; }
    public string DefaultValue { get; set; }
    public bool? RequiresEncryption { get; set; }

    public virtual ThirdPartyService ThirdPartyService { get; set; }
}
