﻿using InvoizR.Application.Helpers;
using InvoizR.Domain.Entities;
using InvoizR.SharedKernel.Mh.FeNr;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Services;

public sealed class Dte04ProcessingStatusChanger : DteProcessingStatusChanger
{
    private readonly ILogger logger;

    public Dte04ProcessingStatusChanger(ILogger<Dte04ProcessingStatusChanger> logger)
        : base(logger)
    {
    }

    protected override bool Init(Invoice invoice)
    {
        FeNrv3 dte;

        try
        {
            dte = FeNrv3.Deserialize(invoice.Payload);
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
