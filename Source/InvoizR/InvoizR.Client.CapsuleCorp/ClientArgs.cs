﻿namespace InvoizR.Client.CapsuleCorp;

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
                ProcessingType = item.Replace("--processing-type=", "");

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

            if (item.StartsWith("--invoice-type"))
                InvoiceType = item.Replace("--invoice-type=", "");
        }
    }

    public bool ShowCatalog { get; }
    public bool Seed { get; }
    public bool Mock { get; }
    public string ProcessingType { get; set; }
    public int Limit { get; }
    public string InvoiceType { get; set; }

    public bool IsRt
        => string.Compare(ProcessingType, "rt", true) == 0;

    public int MockDelay
        => 3000;
}
