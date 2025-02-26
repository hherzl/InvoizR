namespace InvoizR.GUI.InvoiceManager.Client;

public enum InvoiceSyncStatus : short
{
    Created = 0,
    InvalidData = 1,
    Requested = 10,
    Declined = 20,
    Processed = 30,
    Notified = 40,
    Cancelled = 45
}
