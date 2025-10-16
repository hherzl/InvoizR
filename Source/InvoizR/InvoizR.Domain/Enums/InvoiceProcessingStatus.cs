namespace InvoizR.Domain.Enums;

public enum InvoiceProcessingStatus : short
{
    Created = 0,
    InvalidSchema = 100,
    InvalidData = 200,
    Initialized = 500,
    Requested = 1000,
    Declined = 2000,
    Processed = 3000,
    Cancelled = 3500,
    Notified = 4000
}
