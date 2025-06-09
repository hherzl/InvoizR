namespace InvoizR.SharedKernel.Mh;

public static partial class MhCatalog
{
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
                BilletesYMonedas => "Efetivo",
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
