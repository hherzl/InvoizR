using System.Collections.ObjectModel;
using System.Diagnostics;
using InvoizR.Domain.Common;

namespace InvoizR.Domain.Entities;

[DebuggerDisplay("EnvironmentId={EnvironmentId}, Name={Name}")]
public partial class ThirdPartyService : AuditableEntity
{
    public ThirdPartyService()
    {
        ThirdPartyServiceParameters = [];
    }

    public ThirdPartyService(short? id)
        : this()
    {
        Id = id;
    }

    public short? Id { get; set; }
    public string EnvironmentId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual Collection<ThirdPartyServiceParameter> ThirdPartyServiceParameters { get; set; }
    public virtual Collection<CompanyThirdPartyServiceParameter> CompanyThirdPartyServiceParameters { get; set; }
}
