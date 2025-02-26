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
}
