using InvoizR.Application.Services;

namespace InvoizR.Application.UnitTests;

public class InvoiceCodesTests
{
    [Fact]
    public void GenerateInvoiceCodes()
    {
        short type = 1;
        var branch = "S001";
        var pos = "P001";
        var invoiceNumber = 12345;

        var result = new InvoiceCodeGenerator().Generate(type, branch, pos, invoiceNumber);

        Assert.Equal("DTE-01-ES01POS01-000000000012345", result.AuditNumber);
    }

    [Fact]
    public void GenerateInvoiceCodesForUnsupported()
    {
        short type = 0;
        var branch = "";
        var pos = "";
        var invoiceNumber = 0;

        var result = new InvoiceCodeGenerator().Generate(type, branch, pos, invoiceNumber);

        Assert.Null(result.AuditNumber);
    }
}
