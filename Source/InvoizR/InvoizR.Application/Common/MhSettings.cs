namespace InvoizR.Application.Common;

public record MhSettings
{
    public string Environment { get; set; }
    public string User { get; set; }
    public string Pwd { get; set; }
    public string PrivateKey { get; set; }
    public string ExternalUrl { get; set; }
}
