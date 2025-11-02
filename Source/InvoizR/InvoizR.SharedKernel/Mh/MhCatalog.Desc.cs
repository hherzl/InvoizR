namespace InvoizR.SharedKernel.Mh;

public static partial class MhCatalog
{
    public static partial class Cat001
    {
        public static string Desc(string value)
            => value switch
            {
                Prueba => "Prueba",
                Produccion => "Producción",
                _ => ""
            };
    }

    public static partial class Cat002
    {
        public static string Desc(string value)
            => value switch
            {
                Factura => "Factura",
                CreditoFiscal => "Comprobante de Crédito Fiscal",
                NotaRemision => "Nota de Remisión",
                NotaCredito => "Nota de Crédito",
                NotaDebito => "Nota de Débito",
                Exportacion => "Nota de Exportación",
                SujetoExcluido => "Factura de Sujeto Excluido",
                _ => ""
            };
    }

    public static partial class Cat003
    {
        public static string Desc(double value)
            => value switch
            {
                Previo => "Previo",
                Contingencia => "Contingencia",
                _ => ""
            };
    }

    public static partial class Cat004
    {
        public static string Desc(int value)
            => value switch
            {
                Normal => "Normal",
                Contingencia => "Contingencia",
                _ => ""
            };
    }

    public static partial class Cat005
    {
        public static string Desc(int value)
            => value switch
            {
                NoDisponibilidadSistemaMH => "No Disponibilidad del Sistema del MH",
                NoDisponibilidadSistemaEmisor => "No Disponibilidad del Sistema del Emisor",
                FallaSuministroServicioInternetEmisor => "Falla en Suministro de Servicio de Internet del Emisor",
                FallaSuministroServicioEnergiaElectricaEmisor => "Falla en Suministro de Servicio de Energia Eléctrica del Emisor",
                Otro => "Otro",
                _ => ""
            };
    }

    public static partial class Cat007
    {
        public static string Desc(int value)
            => value switch
            {
                Fisico => "Físico",
                Electronico => "Electrónico",
                _ => ""
            };
    }

    public static partial class Cat016
    {
        public static string Desc(double value)
            => value switch
            {
                Contado => "Contado",
                ACredito => "A Crédito",
                Otro => "Otro",
                _ => ""
            };
    }

    public static partial class Cat017
    {
        public static string Desc(string value)
            => value switch
            {
                BilletesYMonedas => "Efectivo",
                TarjetaDebito => "Tarjeta de débito",
                TarjetaCredito => "Tarjeta de crédito",
                _ => ""
            };
    }

    public static partial class Cat022
    {
        public static string Desc(string value)
            => value switch
            {
                CarnetDeResidente => "Carné de Residente",
                Dui => "DUI",
                Nit => "NIT",
                Otro => "Otro",
                Pasaporte => "Pasaporte",
                _ => ""
            };
    }
}
