using InvoizR.Application.Common.Contracts;
using InvoizR.Domain.Enums;
using InvoizR.SharedKernel.Mh;

namespace InvoizR.API.Billing.Services;

public class InvoizRInitializer
{
    private readonly IInvoizRDbContext _dbContext;

    public InvoizRInitializer(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        if (!_dbContext.InvoiceType.Any())
        {
            _dbContext.InvoiceType.Add(new(1, "Consumidor Final", FeFcv1.SchemaType, FeFcv1.Version, true));

            await _dbContext.SaveChangesAsync();
        }

        if (!_dbContext.EnumDescription.Any())
        {
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.Created, "Creada", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.InvalidSchema, "Esquema inválido", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.InvalidData, "Datos inválidos", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.Initialized, "Inicializada", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.Requested, "Solicitada", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.Declined, "Rechazada", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.Processed, "Procesada", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.Notified, "Notificada", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.Cancelled, "Inválida", typeof(InvoiceProcessingStatus).FullName));

            await _dbContext.SaveChangesAsync();
        }
    }
}
