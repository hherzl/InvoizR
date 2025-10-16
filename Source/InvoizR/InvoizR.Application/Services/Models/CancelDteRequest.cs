using System.Collections.ObjectModel;
using InvoizR.Application.Common.Contracts;
using InvoizR.Domain.Entities;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh.Anulacion;

namespace InvoizR.Application.Services.Models;

public record CancelDteRequest : ICancelDteRequest
{
    public CancelDteRequest(IEnumerable<ThirdPartyClientParameter> thirdPartyClientParameters, long? invoiceId, string payload, Responsible responsible)
    {
        ThirdPartyClientParameters = [.. thirdPartyClientParameters];
        InvoiceId = invoiceId;
        Anulacion = Anulacionv2.Deserialize(payload);

        Anulacion.Motivo.TipDocResponsable = responsible.IdType;
        Anulacion.Motivo.NumDocResponsable = responsible.IdNumber;
        Anulacion.Motivo.NombreResponsable = responsible.Name;
    }

    public Collection<ThirdPartyClientParameter> ThirdPartyClientParameters { get; }
    public long? InvoiceId { get; }
    public Anulacionv2 Anulacion { get; }
}
