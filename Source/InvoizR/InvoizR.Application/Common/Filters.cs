using System.Collections.ObjectModel;
using InvoizR.Domain.Enums;

namespace InvoizR.Application.Common;

public class Filters
{
    public Filters()
    {
        SyncStatuses = [];
    }

    public Filters(short invoiceTypeId)
        : this()
    {
        InvoiceTypeId = invoiceTypeId;
    }

    public short? InvoiceTypeId { get; set; }
    public short? ProcessingTypeId { get; set; }
    public Collection<short?> SyncStatuses { get; set; }

    public Filters Add(params IEnumerable<SyncStatus> syncStatuses)
    {
        foreach (var item in syncStatuses)
        {
            SyncStatuses.Add((short)item);
        }

        return this;
    }

    public Filters Set(InvoiceProcessingType processingType)
    {
        ProcessingTypeId = (short)processingType;

        return this;
    }
}
