using InvoizR.SharedKernel.Mh.FeCcf;
using InvoizR.SharedKernel.Mh.FeFc;

namespace InvoizR.Application.Helpers;

public static class DteInfoHelper
{
    public static DteInfoResult Get(short? type, string prefix, string branch, string pos, long? invoiceNumber)
    {
        if (type == FeFcv1.TypeId)
            return new(FeFcv1.TypeId, FeFcv1.SchemaType, Guid.NewGuid().ToString().ToUpper(), $"DTE-{FeFcv1.SchemaType}-{prefix}{branch}{pos}-{invoiceNumber:000000000000000}");
        else if (type == FeCcfv3.TypeId)
            return new(FeCcfv3.TypeId, FeCcfv3.SchemaType, Guid.NewGuid().ToString().ToUpper(), $"DTE-{FeCcfv3.SchemaType}-{prefix}{branch}{pos}-{invoiceNumber:000000000000000}");

        return DteInfoResult.Empty();
    }
}
