namespace InvoizR.Application.Helpers;

public record DteInfoResult
{
    public DteInfoResult()
    {
    }

    public DteInfoResult(short version, string type, string generationCode, string controlNumber)
    {
        Version = version;
        Type = type;
        GenerationCode = generationCode;
        ControlNumber = controlNumber;
    }

    public short Version { get; set; }
    public string Type { get; set; }
    public string GenerationCode { get; set; }
    public string ControlNumber { get; set; }

    public bool HasControlNumber
        => !string.IsNullOrEmpty(ControlNumber);

    public static DteInfoResult Empty()
        => new();
}
