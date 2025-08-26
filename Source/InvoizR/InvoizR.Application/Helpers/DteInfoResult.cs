namespace InvoizR.Application.Helpers;

public record DteInfoResult
{
    public DteInfoResult()
    {
    }

    public DteInfoResult(short version, string type, Guid generationCode, string controlNumber)
    {
        Version = version;
        Type = type;
        GenerationCode = generationCode.ToString().ToUpper();
        ControlNumber = controlNumber;
    }

    public short Version { get; }
    public string Type { get; }
    public string GenerationCode { get; }
    public string ControlNumber { get; }

    public bool HasControlNumber
        => !string.IsNullOrEmpty(ControlNumber);

    public static DteInfoResult Empty()
        => new();
}
