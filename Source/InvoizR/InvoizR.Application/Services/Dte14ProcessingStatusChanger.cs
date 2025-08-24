﻿using InvoizR.Application.Helpers;
using InvoizR.Domain.Entities;
using InvoizR.SharedKernel.Mh.FeFse;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Services;

public sealed class Dte14ProcessingStatusChanger : DteProcessingStatusChanger
{
    private readonly ILogger logger;

    public Dte14ProcessingStatusChanger(ILogger<Dte14ProcessingStatusChanger> logger)
        : base(logger)
    {
    }

    protected override bool Init(Invoice invoice)
    {
        FeFsev1 dte;

        try
        {
            dte = FeFsev1.Deserialize(invoice.Payload);
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, $"There was an error deserializing {invoice.InvoiceNumber} invoice");
            return false;
        }

        dte.Identificacion.Version = (int)invoice.SchemaVersion;
        dte.Identificacion.TipoDte = invoice.SchemaType;
        dte.Identificacion.CodigoGeneracion = invoice.GenerationCode;
        dte.Identificacion.NumeroControl = invoice.ControlNumber;

        dte.Resumen.TotalLetras = MoneyToWordsConverter.SpellingNumber(invoice.InvoiceTotal);

        invoice.Payload = dte.ToJson();

        return true;
    }
}
