namespace InvoizR.Domain.Enums;

public enum SyncStatus : short
{
    Created = 0,
    InvalidSchema = 100,
    InvalidData = 200,
    Initialized = 500,
    Fallback = 600,
    Requested = 1000,
    Declined = 2000,
    Processed = 3000,
    Notified = 4000,
    Cancelled = 5000,
}
