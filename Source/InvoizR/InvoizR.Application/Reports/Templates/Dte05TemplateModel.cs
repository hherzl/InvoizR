using System.Collections.ObjectModel;
using InvoizR.Application.Reports.Templates.Common;
using InvoizR.SharedKernel.Mh.FeNc;

namespace InvoizR.Application.Reports.Templates;

public record Dte05TemplateModel : DteTemplateModel<FeNcv3>
{
    public Dte05TemplateModel()
    {
        ReferencedDocuments = [];
    }

    public Collection<ReferencedDocumentModel> ReferencedDocuments { get; set; }
}
