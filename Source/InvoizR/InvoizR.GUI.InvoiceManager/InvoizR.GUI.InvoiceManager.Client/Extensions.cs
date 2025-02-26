using MudBlazor;

namespace InvoizR.GUI.InvoiceManager.Client;

public static class Extensions
{
    public static string ToCurrency(this decimal? amount)
    {
        if (amount == null)
            return "0.0";

        return amount.Value.ToString("C2");
    }

    public static (Color, Variant) GetButtonStyle(this short? syncStatusId)
    {
        return syncStatusId switch
        {
            0 => (Color.Default, Variant.Text),
            100 => (Color.Warning, Variant.Text),
            200 => (Color.Warning, Variant.Text),
            500 => (Color.Secondary, Variant.Outlined),
            1000 => (Color.Secondary, Variant.Outlined),
            2000 => (Color.Error, Variant.Filled),
            3000 => (Color.Success, Variant.Outlined),
            4000 => (Color.Success, Variant.Filled),
            _ => (Color.Default, Variant.Text)
        };
    }
}
