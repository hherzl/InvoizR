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

            if (item.StartsWith("--processing-type"))
                ProcessingType = args[1].Replace("--processing-type=", "");

            if (item.StartsWith("--limit"))
            {
                try
                {
                    Limit = Convert.ToInt32(item.Replace("--limit=", ""));
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
    public string ProcessingType { get; set; }
    public int Limit { get; }

    public bool IsRt
        => string.Compare(ProcessingType, "RT", true) == 0;
}
