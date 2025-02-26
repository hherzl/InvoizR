using InvoizR.Clients.DataContracts.Common;

namespace InvoizR.Clients.DataContracts;

public record GetInvoicesViewBagResponse
{
    public List<ListItem<int>> PageSizes { get; set; }
    public List<ListItem<short?>> BranchPos { get; set; }
    public List<ListItem<short?>> ProcessingStatuses { get; set; }
}
