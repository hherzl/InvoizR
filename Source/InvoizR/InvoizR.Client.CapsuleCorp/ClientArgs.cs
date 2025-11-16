namespace InvoizR.Client.CapsuleCorp;

public record ClientArgs
{
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

            if (item.StartsWith("--invoice-type"))
                InvoiceType = item.Replace("--invoice-type=", "");

            if (item.StartsWith("--processing-type"))
                ProcessingType = item.Replace("--processing-type=", "");

            if (item.StartsWith("--limit"))
            {
                if (int.TryParse(item.Replace("--limit=", ""), out var limit))
                    Limit = limit;
            }

            if (item == "--fallback")
                Fallback = true;

            if (Fallback && item == "--process")
                FallbackProcess = true;

            if (item == "--webhook")
                Webhook = true;
        }
    }

    public bool ShowCatalog { get; }
    public bool Seed { get; }
    public bool Mock { get; }

    public string BillingEndpoint = "https://localhost:13880";

    public string InvoiceType { get; set; }
    public string ProcessingType { get; set; }
    public int Limit { get; }

    public bool IsRt
        => string.Compare(ProcessingType, "rt", true) == 0;

    public bool Fallback { get; set; }
    public bool FallbackProcess { get; set; }

    public bool Webhook { get; set; }

    public static int MockDelay
        => 3000;
}
