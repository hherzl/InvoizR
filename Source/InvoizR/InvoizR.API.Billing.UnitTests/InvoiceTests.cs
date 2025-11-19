using InvoizR.Clients.DataContracts.Invoices;

namespace InvoizR.API.Billing.UnitTests;

public class InvoiceTests : BillingTest
{
    public InvoiceTests()
        : base()
    {
    }

    [Fact]
    public async Task GetInvoices_RetrieveList_ReturnsOk()
    {
        // Arrange
        var query = new GetInvoicesQuery();

        // Act
        var response = await _mediator.Send(query);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.Model.Count >= 0);
        Assert.Equal(10, response.PageSize);
        Assert.Equal(1, response.PageNumber);
        Assert.Equal(100, response.ItemsCount);
        Assert.Equal(10, response.PageCount);
    }

    [Fact]
    public async Task GetInvoice_RetrieveExisting_ReturnsOk()
    {
        // Arrange
        var query = new GetInvoiceQuery(1);

        // Act
        var response = await _mediator.Send(query);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task GetInvoice_RetrieveNonExisting_ReturnsNotFound()
    {
        // Arrange
        var query = new GetInvoiceQuery(0);

        // Act
        var response = await _mediator.Send(query);

        // Assert
        Assert.Null(response);
    }
}
