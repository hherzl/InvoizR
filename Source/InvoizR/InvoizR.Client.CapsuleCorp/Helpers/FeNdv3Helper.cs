using InvoizR.Client.CapsuleCorp.Mocks;
using InvoizR.Clients.DataContracts.Dte06;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh;
using InvoizR.SharedKernel.Mh.FeNd;

namespace InvoizR.Client.CapsuleCorp.Helpers;

public static class FeNdv3Helper
{
    public static FeNdv3 Create(CreateDte06Command request, IEnumerable<InvoiceLine> lines)
    {
        var dte = new FeNdv3();

        dte.Identificacion.Version = FeNdv3.Version;
        dte.Identificacion.Ambiente = MhCatalog.Cat001.Prueba;
        dte.Identificacion.TipoDte = FeNdv3.SchemaType;
        dte.Identificacion.FecEmi = new DateTimeOffset(request.InvoiceDate.Value);
        dte.Identificacion.HorEmi = dte.Identificacion.FecEmi.ToString("hh:mm:ss");
        dte.Identificacion.TipoMoneda = IdentificacionTipoMoneda.USD;
        dte.Identificacion.TipoModelo = MhCatalog.Cat003.Previo;
        dte.Identificacion.TipoOperacion = MhCatalog.Cat004.Normal;

        var emisor = CompanyMocks.GetCapsuleCorp();

        dte.Emisor.Nit = emisor.TaxIdNumber;
        dte.Emisor.Nrc = emisor.TaxpayerRegistrationNumber;
        dte.Emisor.Nombre = emisor.BusinessName;
        dte.Emisor.CodActividad = emisor.EconomicActivityId;
        dte.Emisor.DescActividad = emisor.EconomicActivity;
        dte.Emisor.Direccion = new Direccion
        {
            Departamento = MhCatalog.Cat012.SanSalvador,
            Municipio = MhCatalog.Cat013.SanSalvador,
            Complemento = emisor.Address
        };

        dte.Emisor.TipoEstablecimiento = MhCatalog.Cat009.SucursalAgencia;
        dte.Emisor.Telefono = emisor.Phone;
        dte.Emisor.Correo = emisor.Email;

        var total = Math.Round(lines.Sum(item => item.Total), 2);
        var vatSum = 0;

        dte.Receptor = new()
        {
            Nit = request.Customer.DocumentNumber,
            Nrc = "NRC",
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
                VentaNoSuj = 0,
                VentaExenta = 0,
                VentaGravada = line.Total,
                Tributos = [MhCatalog.Cat015.ImpuestoValorAgregado]
            });
        }

        dte.Resumen.CondicionOperacion = MhCatalog.Cat016.Contado;

        dte.Resumen.SubTotalVentas = total;
        dte.Resumen.TotalGravada = total;
        dte.Resumen.SubTotal = total;

        dte.Resumen.MontoTotalOperacion = total;

        dte.Resumen.IvaPerci1 = vatSum;

        dte.DocumentoRelacionado.Add(new()
        {
            TipoDocumento = MhCatalog.Cat002.CreditoFiscal,
            TipoGeneracion = MhCatalog.Cat007.Electronico,
            NumeroDocumento = Guid.NewGuid().ToString().ToUpper(),
            FechaEmision = DateTime.Now.AddDays(-5)
        });

        return dte;
    }
}
