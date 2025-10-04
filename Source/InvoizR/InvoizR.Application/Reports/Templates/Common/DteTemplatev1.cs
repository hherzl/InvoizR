namespace InvoizR.Application.Reports.Templates.Common;

public class DteTemplatev1<TDte>
{
    public DteTemplatev1(TDte model)
    {
        Model = model;
    }

    public TDte Model { get; }

    protected string AsStrong(string value)
            => $"<strong>{value}</strong>";

    protected string AsDate(DateTime? value)
        => $"{value:yyyy-MM-dd}";

    protected string AsDateTime(DateTime? value)
        => $"{value:yyyy-MM-dd hh:mm:ss}";

    protected string AsAmount(double amount)
            => $"{amount:N2}";
}
