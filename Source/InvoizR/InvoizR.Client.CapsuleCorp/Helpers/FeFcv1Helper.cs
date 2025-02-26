using InvoizR.Client.CapsuleCorp.Mocks;
using InvoizR.Clients.DataContracts;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh;

namespace InvoizR.Client.CapsuleCorp.Helpers;

public record CreateFeFcv1Request
{
    public CustomerNode Customer { get; set; }
    public DateTime? InvoiceDate { get; set; }

    public IEnumerable<InvoiceLine> InvoiceLines { get; set; }
}

public static class FeFcv1Helper
{
    public static FeFcv1 Create(CreateDte01InvoiceCommand request, List<InvoiceLine> lines)
    {
        var eDoc = new FeFcv1();

        eDoc.Identificacion.Version = FeFcv1.Version;
        eDoc.Identificacion.Ambiente = MhCatalog.Cat001.Prueba;

        eDoc.Identificacion.FecEmi = new DateTimeOffset(request.InvoiceDate.Value);
        eDoc.Identificacion.HorEmi = eDoc.Identificacion.FecEmi.ToString("hh:mm:ss");
        eDoc.Identificacion.TipoMoneda = IdentificacionTipoMoneda.USD;
        eDoc.Identificacion.TipoModelo = MhCatalog.Cat003.Previo;
        eDoc.Identificacion.TipoModelo = MhCatalog.Cat004.Normal;

        var emisor = CompanyMocks.GetCapsuleCorp();

        eDoc.Emisor.Nit = emisor.TaxIdNumber;
        eDoc.Emisor.Nrc = emisor.TaxRegistrationNumber;
        eDoc.Emisor.Nombre = emisor.BusinessName;
        eDoc.Emisor.CodActividad = emisor.EconomicActivityId;
        eDoc.Emisor.DescActividad = emisor.EconomicActivity;
        eDoc.Emisor.Direccion = new Direccion
        {
            Departamento = MhCatalog.Cat012.SanSalvador,
            Municipio = MhCatalog.Cat013.SanSalvador,
            Complemento = emisor.Address
        };

        eDoc.Emisor.TipoEstablecimiento = MhCatalog.Cat009.SucursalAgencia;
        eDoc.Emisor.Telefono = emisor.Phone;
        eDoc.Emisor.Correo = emisor.Email;
        //eDoc.Emisor.CodEstable = $"{request.Document.Branch.EstablishmentPrefix}{request.Document.Branch.WhsCode}";
        //eDoc.Emisor.CodPuntoVenta = request.Document.Branch.Pos;

        var total = Math.Round(lines.Sum(item => item.Total), 2);
        var vatSum = 0;

        eDoc.Receptor = new()
        {
            Nombre = request.Customer.Name,
            TipoDocumento = request.Customer.DocumentTypeId,
            NumDocumento = request.Customer.DocumentNumber,
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
            eDoc.CuerpoDocumento.Add(new CuerpoDocumento
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
                IvaItem = line.VatSum
            });
        }

        eDoc.Resumen.CondicionOperacion = MhCatalog.Cat016.Contado;

        eDoc.Resumen.SubTotalVentas = total;
        eDoc.Resumen.TotalGravada = total;
        eDoc.Resumen.SubTotal = total;

        eDoc.Resumen.MontoTotalOperacion = total;

        eDoc.Resumen.TotalPagar = total;

        eDoc.Resumen.TotalIva = vatSum;

        eDoc.Resumen.Pagos =
        [
            new() { Codigo = MhCatalog.Cat017.TarjetaCredito, MontoPago = eDoc.Resumen.TotalPagar }
        ];

        return eDoc;
    }
}
