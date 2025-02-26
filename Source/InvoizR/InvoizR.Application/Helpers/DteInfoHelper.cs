using InvoizR.SharedKernel.Mh;

namespace InvoizR.Application.Helpers;

public static class DteInfoHelper
{
    public static DteInfoResult Get(short? type, string prefix, string branch, string pos, long? invoiceNumber)
    {
        if (type == 1)
            return new(1, "01", Guid.NewGuid().ToString().ToUpper(), $"DTE-01-{prefix}{branch}{pos}-{invoiceNumber:000000000000000}");

        return DteInfoResult.Empty();
    }
}
