using System.Collections.ObjectModel;
using InvoizR.Domain.Enums;

namespace InvoizR.Application.Common;

public class Filters
{
    public Filters()
    {
        ProcessingStatuses = [];
    }

    public Filters(short invoiceTypeId)
        : this()
    {
        InvoiceTypeId = invoiceTypeId;
    }

    public short? InvoiceTypeId { get; set; }
    public short? ProcessingTypeId { get; set; }
    public Collection<short?> ProcessingStatuses { get; set; }

    public Filters Add(params IEnumerable<InvoiceProcessingStatus> processingStatuses)
    {
        foreach (var item in processingStatuses)
        {
            ProcessingStatuses.Add((short)item);
        }

        return this;
    }

    public Filters Set(InvoiceProcessingType invoiceProcessingType)
    {
        ProcessingTypeId = (short)invoiceProcessingType;

        return this;
    }
}
