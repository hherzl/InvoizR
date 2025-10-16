namespace InvoizR.Domain.Entities;

public partial class InvoiceType
{
    public InvoiceType(short? id, string name, string schemaType, short? schemaVersion, bool current, short? cancellationPeriodInDays)
    {
        Id = id;
        Name = name;
        SchemaType = schemaType;
        SchemaVersion = schemaVersion;
        Current = current;
        CancellationPeriodInDays = cancellationPeriodInDays;
    }
}
