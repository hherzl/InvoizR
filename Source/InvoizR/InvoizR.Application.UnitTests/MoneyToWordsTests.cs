using InvoizR.Application.Helpers;

namespace InvoizR.Application.UnitTests;

public class MoneyToWordsTests
{
    [Fact]
    public void _100ToMoneyToWords()
    {
        // Arrange
        decimal? amount = 100;

        // Act
        var words = MoneyToWordsConverter.SpellingNumber(amount);

        // Assert
        Assert.Equal("CIEN D�LARES 00/100", words);
    }

    [Fact]
    public void _1000ToMoneyToWords()
    {
        // Arrange
        decimal? amount = 1000;

        // Act
        var words = MoneyToWordsConverter.SpellingNumber(amount);

        // Assert
        Assert.Equal("MIL D�LARES 00/100", words);
    }
}
