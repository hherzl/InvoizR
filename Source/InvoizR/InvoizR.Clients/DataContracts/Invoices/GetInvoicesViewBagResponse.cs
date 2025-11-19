using InvoizR.Clients.DataContracts.Common;

namespace InvoizR.Clients.DataContracts.Invoices;

public record GetInvoicesViewBagResponse
{
    public List<ListItem<int>> PageSizes { get; set; }
    public List<ListItem<short?>> BranchPos { get; set; }
    public List<ListItem<short?>> InvoiceTypes { get; set; }
    public List<ListItem<short?>> ProcessingTypes { get; set; }
    public List<ListItem<short?>> SyncStatuses { get; set; }
}
