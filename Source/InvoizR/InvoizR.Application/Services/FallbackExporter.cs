using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.Contracts;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Services;

public sealed class FallbackExporter(ILogger<FallbackExporter> logger, IServiceProvider serviceProvider, IConfiguration configuration)
{
    public async Task ExportAsync(short? fallbackId, CancellationToken ct = default)
    {
        using var serviceScope = serviceProvider.CreateScope();

        using var dbContext = serviceScope.ServiceProvider.GetRequiredService<IInvoizRDbContext>();
        var exportStrategies = serviceScope.ServiceProvider.GetServices<IFallbackExportStrategy>();
        var smtpClient = serviceScope.ServiceProvider.GetRequiredService<ISmtpClient>();

        var fallback = await dbContext.GetFallbackAsync(fallbackId, true, false, ct);

        var processingSettings = new ProcessingSettings();
        configuration.Bind("ProcessingSettings", processingSettings);

        foreach (var exportStrategy in exportStrategies)
        {
            logger.LogInformation($"Exporting '{fallback.Name}-{fallback.FallbackGuid}' fallback as '{exportStrategy.FileExtension}'...");

            var bytes = await exportStrategy.ExportAsync(fallback, processingSettings.GetFallbackPath(fallback.FallbackGuid, exportStrategy.FileExtension), ct);

            logger.LogInformation($" Adding '{exportStrategy.FileExtension}' as bytes...");

            var onTheFlyFile = new FallbackFile(fallback, bytes, exportStrategy.ContentType, exportStrategy.FileExtension);
            var existingFile = await dbContext.FallbackFile.FirstOrDefaultAsync(item => item.FallbackId == fallback.Id && item.FileName == onTheFlyFile.FileName, ct);
            if (existingFile == null)
                dbContext.FallbackFile.Add(onTheFlyFile);
            else
                existingFile.File = onTheFlyFile.File;
        }

        fallback.EndDateTime = DateTime.Now;
        fallback.Enable = false;
        fallback.SyncStatusId = (short)SyncStatus.Notified;

        await dbContext.SaveChangesAsync(ct);
    }
}
