using InvoizR.Clients.DataContracts;

namespace InvoizR.API.Billing.UnitTests;

public class CompanyTests : BillingTest
{
    public CompanyTests()
        : base()
    {
    }

    [Fact]
    public async Task GetCompanies_RetrieveList_ReturnsOk()
    {
        // Arrange
        var query = new GetCompaniesQuery();

        // Act
        var response = await _mediator.Send(query);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.Model.Count() >= 0);
    }

    [Fact]
    public async Task GetCompany_RetrieveExisting_ReturnsOk()
    {
        // Arrange
        var query = new GetCompanyQuery(1);

        // Act
        var response = await _mediator.Send(query);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task GetCompany_RetrieveNonExisting_ReturnsNotFound()
    {
        // Arrange
        var query = new GetCompanyQuery(0);

        // Act
        var response = await _mediator.Send(query);

        // Assert
        Assert.Null(response);
    }
}
