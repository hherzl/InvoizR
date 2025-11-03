namespace InvoizR.Application.Helpers;

public record DteInfoResult
{
    public DteInfoResult()
    {
    }

    public DteInfoResult(short version, string type, Guid generationCode, string auditNumber)
    {
        Version = version;
        Type = type;
        GenerationCode = generationCode.ToString().ToUpper();
        AuditNumber = auditNumber;
    }

    public short Version { get; }
    public string Type { get; }
    public string GenerationCode { get; }
    public string AuditNumber { get; }

    public bool HasAuditNumber
        => !string.IsNullOrEmpty(AuditNumber);

    public static DteInfoResult Empty()
        => new();
}
