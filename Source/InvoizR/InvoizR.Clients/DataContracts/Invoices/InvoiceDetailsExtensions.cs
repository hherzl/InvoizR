using InvoizR.SharedKernel.Mh.FeCcf;
using InvoizR.SharedKernel.Mh.FeFc;
using InvoizR.SharedKernel.Mh.FeFse;
using InvoizR.SharedKernel.Mh.FeNr;

namespace InvoizR.Clients.DataContracts.Invoices;

public static class InvoiceDetailsExtensions
{
    public static bool IsDte01(this InvoiceDetailsModel detailsModel)
        => detailsModel.InvoiceTypeId == FeFcv1.TypeId;

    public static bool IsDte03(this InvoiceDetailsModel detailsModel)
        => detailsModel.InvoiceTypeId == FeCcfv3.TypeId;

    public static bool IsDte04(this InvoiceDetailsModel detailsModel)
        => detailsModel.InvoiceTypeId == FeNrv3.TypeId;

    public static bool IsDte14(this InvoiceDetailsModel detailsModel)
        => detailsModel.InvoiceTypeId == FeFsev1.TypeId;
}
