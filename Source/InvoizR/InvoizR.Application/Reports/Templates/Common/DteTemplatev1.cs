namespace InvoizR.Application.Reports.Templates.Common;

public class DteTemplatev1<TDte>
{
    public DteTemplatev1(TDte model)
    {
        Model = model;
    }

    public TDte Model { get; }
}
