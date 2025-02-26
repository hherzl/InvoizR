﻿using InvoizR.Application.Helpers;

namespace InvoizR.Application.UnitTests;

public class DteInfoTests
{
    [Fact]
    public void GetDteInfo()
    {
        short type = 1;
        var prefix = "ES";
        var branch = "01";
        var pos = "POS01";
        var invoiceNumber = 12345;

        var result = DteInfoHelper.Get(type, prefix, branch, pos, invoiceNumber);

        Assert.Equal("DTE-01-ES01POS01-000000000012345", result.ControlNumber);
    }

    [Fact]
    public void GetDteInfoForUnsupported()
    {
        short type = 0;
        var prefix = "";
        var branch = "";
        var pos = "";
        var invoiceNumber = 0;

        var result = DteInfoHelper.Get(type, prefix, branch, pos, invoiceNumber);

        Assert.Null(result.ControlNumber);
    }
}
