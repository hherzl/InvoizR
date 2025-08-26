using InvoizR.SharedKernel.Mh.FeCcf;
using InvoizR.SharedKernel.Mh.FeFc;
using InvoizR.SharedKernel.Mh.FeFse;
using InvoizR.SharedKernel.Mh.FeNr;

namespace InvoizR.Application.Helpers;

public static class DteInfoHelper
{
    public static DteInfoResult Get(short? type, string prefix, string branch, string pos, long? invoiceNumber)
    {
        if (type == FeFcv1.TypeId)
            return new(FeFcv1.TypeId, FeFcv1.SchemaType, Guid.NewGuid(), $"DTE-{FeFcv1.SchemaType}-{prefix}{branch}{pos}-{invoiceNumber:000000000000000}");
        else if (type == FeCcfv3.TypeId)
            return new(FeCcfv3.TypeId, FeCcfv3.SchemaType, Guid.NewGuid(), $"DTE-{FeCcfv3.SchemaType}-{prefix}{branch}{pos}-{invoiceNumber:000000000000000}");
        else if (type == FeNrv3.TypeId)
            return new(FeNrv3.TypeId, FeNrv3.SchemaType, Guid.NewGuid(), $"DTE-{FeNrv3.SchemaType}-{prefix}{branch}{pos}-{invoiceNumber:000000000000000}");
        else if (type == FeFsev1.TypeId)
            return new(FeFsev1.TypeId, FeFsev1.SchemaType, Guid.NewGuid(), $"DTE-{FeFsev1.SchemaType}-{prefix}{branch}{pos}-{invoiceNumber:000000000000000}");

        return DteInfoResult.Empty();
    }
}
