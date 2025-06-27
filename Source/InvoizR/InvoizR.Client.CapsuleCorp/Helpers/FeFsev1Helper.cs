using InvoizR.Client.CapsuleCorp.Mocks;
using InvoizR.Clients.DataContracts.Dte14;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh;
using InvoizR.SharedKernel.Mh.FeFse;

namespace InvoizR.Client.CapsuleCorp.Helpers;

public static class FeFsev1Helper
{
    public static FeFsev1 Create(CreateDte14Command request, List<InvoiceLine> lines)
    {
        var dte = new FeFsev1();

        dte.Identificacion.Version = FeFsev1.Version;
        dte.Identificacion.Ambiente = MhCatalog.Cat001.Prueba;
        dte.Identificacion.TipoDte = FeFsev1.SchemaType;
        dte.Identificacion.FecEmi = new DateTimeOffset(request.InvoiceDate.Value);
        dte.Identificacion.HorEmi = dte.Identificacion.FecEmi.ToString("hh:mm:ss");
        dte.Identificacion.TipoMoneda = IdentificacionTipoMoneda.USD;
        dte.Identificacion.TipoModelo = MhCatalog.Cat003.Previo;
        dte.Identificacion.TipoOperacion = MhCatalog.Cat004.Normal;

        var emisor = CompanyMocks.GetCapsuleCorp();

        dte.Emisor.Nit = emisor.TaxIdNumber;
        dte.Emisor.Nrc = emisor.TaxRegistrationNumber;
        dte.Emisor.Nombre = emisor.BusinessName;
        dte.Emisor.CodActividad = emisor.EconomicActivityId;
        dte.Emisor.DescActividad = emisor.EconomicActivity;
        dte.Emisor.Direccion = new Direccion
        {
            Departamento = MhCatalog.Cat012.SanSalvador,
            Municipio = MhCatalog.Cat013.SanSalvador,
            Complemento = emisor.Address
        };

        dte.Emisor.Telefono = emisor.Phone;
        dte.Emisor.Correo = emisor.Email;

        var total = Math.Round(lines.Sum(item => item.Total), 2);

        dte.SujetoExcluido = new()
        {
            Nombre = request.Customer.Name,
            Direccion = new()
            {
                Departamento = MhCatalog.Cat012.SanSalvador,
                Municipio = MhCatalog.Cat013.SanSalvador,
                Complemento = request.Customer.Address
            },
            Telefono = request.Customer.Phone,
            Correo = request.Customer.Email
        };

        var number = 1;

        foreach (var line in lines)
        {
            dte.CuerpoDocumento.Add(new CuerpoDocumento
            {
                NumItem = number++,
                TipoItem = CuerpoDocumentoTipoItem._1,
                Cantidad = line.Quantity,
                Codigo = line.Code,
                UniMedida = MhCatalog.Cat014.Otra,
                Descripcion = line.Description,
                PrecioUni = line.Price,
                MontoDescu = 0,
                Compra = line.Total
            });
        }

        dte.Resumen.CondicionOperacion = MhCatalog.Cat016.Contado;

        dte.Resumen.SubTotal = total;

        dte.Resumen.TotalPagar = total;

        dte.Resumen.Pagos =
        [
            new() { Codigo = MhCatalog.Cat017.BilletesYMonedas, MontoPago = dte.Resumen.TotalPagar }
        ];

        return dte;
    }
}
