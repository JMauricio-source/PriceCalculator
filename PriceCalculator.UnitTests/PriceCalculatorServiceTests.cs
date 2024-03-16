using FluentAssertions;

using PriceCalculator.Application;
using PriceCalculator.Domain.Entities;
using PriceCalculator.UnitTests.TestData;

namespace PriceCalculator.UnitTests
{
    public class PriceCalculatorServiceTests
    {

        [Theory]
        [MemberData(nameof(AmountTestData.GivenGrossAndVAT), MemberType = typeof(AmountTestData))]
        public void GivenAmountWithVATAndGrossValue_shouldFillCorrectly_All_AmountValues(Amount given, Amount calculated)
        {
            //Arrange
            var sut = new PriceCalculatorService();
            //Act
            Amount result = sut.CalculateAmount(given);

            //Assert
            result.VAT.Should().Be(given.VAT);
            result.GrossValue.Should().Be(given.GrossValue);
            result.NetValue.Should().Be(calculated.NetValue);
            result.VATValue.Should().Be(calculated.VATValue);
        }

        [Theory]
        [MemberData(nameof(AmountTestData.GivenNetAndVAT), MemberType = typeof(AmountTestData))]
        public void GivenAmountWithVATAndNetValue_shouldFillCorrectly_All_AmountValues(Amount given, Amount calculated)
        {
            //Arrange
            var sut = new PriceCalculatorService();
            //Act
            Amount result = sut.CalculateAmount(given);

            //Assert
            result.VAT.Should().Be(given.VAT);
            result.NetValue.Should().Be(given.NetValue);
            result.GrossValue.Should().Be(calculated.GrossValue);
            result.VATValue.Should().Be(calculated.VATValue);
        }

        [Theory]
        [MemberData(nameof(AmountTestData.GivenVATValueAndVAT), MemberType = typeof(AmountTestData))]
        public void GivenAmountWithVATAndVATValue_shouldFillCorrectly_All_AmountValues(Amount given, Amount calculated)
        {
            //Arrange
            var sut = new PriceCalculatorService();
            //Act
            Amount result = sut.CalculateAmount(given);

            //Assert
            result.VAT.Should().Be(given.VAT);
            result.VATValue.Should().Be(given.VATValue);
            result.GrossValue.Should().Be(calculated.GrossValue);
            result.NetValue.Should().Be(calculated.NetValue);
        }
    }
}
