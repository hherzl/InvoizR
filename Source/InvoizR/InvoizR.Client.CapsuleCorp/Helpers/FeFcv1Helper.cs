﻿using InvoizR.Client.CapsuleCorp.Mocks;
using InvoizR.Clients.DataContracts;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh;
using InvoizR.SharedKernel.Mh.FeFc;

namespace InvoizR.Client.CapsuleCorp.Helpers;

public static class FeFcv1Helper
{
    public static FeFcv1 Create(CreateDte01InvoiceCommand request, List<InvoiceLine> lines)
    {
        var dte = new FeFcv1();

        dte.Identificacion.Version = FeFcv1.Version;
        dte.Identificacion.Ambiente = MhCatalog.Cat001.Prueba;
        dte.Identificacion.TipoDte = FeFcv1.SchemaType;
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

        dte.Emisor.TipoEstablecimiento = MhCatalog.Cat009.SucursalAgencia;
        dte.Emisor.Telefono = emisor.Phone;
        dte.Emisor.Correo = emisor.Email;
        //dte.Emisor.CodEstable = $"{request.Document.Branch.EstablishmentPrefix}{request.Document.Branch.WhsCode}";
        //dte.Emisor.CodPuntoVenta = request.Document.Branch.Pos;

        var total = Math.Round(lines.Sum(item => item.Total), 2);
        var vatSum = 0;

        dte.Receptor = new()
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
                IvaItem = line.VatSum
            });
        }

        dte.Resumen.CondicionOperacion = MhCatalog.Cat016.Contado;

        dte.Resumen.SubTotalVentas = total;
        dte.Resumen.TotalGravada = total;
        dte.Resumen.SubTotal = total;

        dte.Resumen.MontoTotalOperacion = total;

        dte.Resumen.TotalPagar = total;

        dte.Resumen.TotalIva = vatSum;

        dte.Resumen.Pagos =
        [
            new() { Codigo = MhCatalog.Cat017.TarjetaCredito, MontoPago = dte.Resumen.TotalPagar }
        ];

        return dte;
    }
}
