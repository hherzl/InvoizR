namespace MH.SharedKernel;

public static class MhDb
{
    private static readonly List<TaxPayer> _taxPayers;

    static MhDb()
    {
        _taxPayers = new()
        {
            new() { Id = "06140101211234", Name = "Capsule Corp." }
        };
    }

    public static IEnumerable<TaxPayer> TaxPayers
        => _taxPayers;
}

public record TaxPayer
{
    public string Id { get; set; }
    public string Name { get; set; }
}
