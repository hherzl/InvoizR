namespace InvoizR.SharedKernel.Mh;

public static partial class MhCatalog
{
    /// <summary>
    /// CAT-001 Ambiente de destino
    /// </summary>
    public static partial class Cat001
    {
        public const string Prueba = "00";
        public const string Produccion = "01";
    }

    /// <summary>
    /// CAT-002 Tipo de Documento
    /// </summary>
    public static partial class Cat002
    {
        public const string Factura = "01";
        public const string CreditoFiscal = "03";
        public const string NotaCredito = "05";
        public const string NotaDebito = "06";
        public const string NotaRemision = "04";
        public const string Exportacion = "11";
        public const string SujetoExcluido = "14";
    }

    /// <summary>
    /// CAT-003 Modelo de Facturación
    /// </summary>
    public static partial class Cat003
    {
        public const int Previo = 1;
        public const int Contingencia = 2;
    }

    /// <summary>
    /// CAT-004 Tipo de Transmisión
    /// </summary>
    public static partial class Cat004
    {
        public const int Normal = 1;
        public const int Contingencia = 2;
    }

    /// <summary>
    /// CAT-005 Tipo de Contingencia
    /// </summary>
    public static partial class Cat005
    {
        public const int NoDisponibilidadSistemaMH = 1;
        public const int NoDisponibilidadSistemaEmisor = 2;
        public const int FallaSuministroServicioInternetEmisor = 3;
        public const int FallaSuministroServicioEnergiaElectricaEmisor = 4;
        public const int Otro = 5;
    }

    /// <summary>
    /// CAT-007 Tipo de Generación del Documento
    /// </summary>
    public static partial class Cat007
    {
        public const int Fisico = 1;
        public const int Electronico = 2;
    }

    /// <summary>
    /// CAT-009 Tipo de Establecimiento
    /// </summary>
    public static class Cat009
    {
        public const string SucursalAgencia = "01";
        public const string CasaMatriz = "02";
        public const string Bodega = "04";
        public const string PredioPatio = "07";
        public const string Otro = "20";
    }

    /// <summary>
    /// CAT-011 Tipo de ítem
    /// </summary>
    public static class Cat011
    {
        public const int Bienes = 1;
        public const int Servicios = 2;
        public const int Ambos = 3;
        public const int Otros = 4;
    }

    /// <summary>
    /// CAT-012 Departamento
    /// </summary>
    public static class Cat012
    {
        public const string SanSalvador = "06";
    }

    /// <summary>
    /// CAT-013 Municipio
    /// </summary>
    public static class Cat013
    {
        public const string SanSalvador = "14";
    }

    /// <summary>
    /// CAT-014 Unidad de Medida
    /// </summary>
    public static class Cat014
    {
        public const int KilometroCuadrado = 9;
        public const int Otra = 99;
    }

    /// <summary>
    /// CAT-015 Tributos
    /// </summary>
    public static class Cat015
    {
        public const string ImpuestoValorAgregado = "20";
    }

    /// <summary>
    /// CAT-016 Condición de la operación
    /// </summary>
    public static partial class Cat016
    {
        public const double Contado = 1;
        public const double ACredito = 2;
        public const double Otro = 3;
    }

    /// <summary>
    /// CAT-017 Forma de pago
    /// </summary>
    public static partial class Cat017
    {
        public const string BilletesYMonedas = "01";
        public const string TarjetaDebito = "02";
        public const string TarjetaCredito = "03";
    }

    /// <summary>
    /// CAT-022 Tipo de documento de identificación del Receptor
    /// </summary>
    public static partial class Cat022
    {
        public const string CarnetDeResidente = "02";
        public const string Dui = "13";
        public const string Nit = "36";
        public const string Otro = "37";
        public const string Pasaporte = "03";
    }
}
