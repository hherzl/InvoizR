using System.Collections.ObjectModel;
using InvoizR.Application.Reports.Templates.Common;
using InvoizR.SharedKernel.Mh.FeNd;

namespace InvoizR.Application.Reports.Templates;

public record Dte06TemplateModel : DteTemplateModel<FeNdv3>
{
    public Dte06TemplateModel()
    {
        ReferencedDocuments = [];
    }

    public Collection<ReferencedDocumentModel> ReferencedDocuments { get; set; }
}
