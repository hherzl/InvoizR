using DinkToPdf.Contracts;

namespace InvoizR.API.Reports.Rendering;

public class DteRender
{
    private readonly IConverter _converter;

    public DteRender(IConverter converter)
    {
        _converter = converter;
    }
}
