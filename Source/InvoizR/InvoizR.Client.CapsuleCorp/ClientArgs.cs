namespace InvoizR.Client.CapsuleCorp;

public record ClientArgs
{
    private readonly int DefaultLimit = 25;

    public ClientArgs(params string[] args)
    {
        foreach (var item in args)
        {
            if (item == "--show-catalog")
                ShowCatalog = true;

            if (item == "--seed")
                Seed = true;

            if (item == "--mock")
                Mock = true;

            if (item.Contains("--limit"))
            {
                try
                {
                    Limit = Convert.ToInt32(args[1].Replace("--limit=", ""));
                }
                catch
                {
                    Limit = DefaultLimit;
                }
            }
        }
    }

    public bool ShowCatalog { get; }
    public bool Seed { get; }
    public bool Mock { get; }
    public int Limit { get; }
}
