using FluentAssertions;
using PriceCalculator.Domain.Entities;
using PriceCalculator.UnitTests.TestData;

namespace PriceCalculator.UnitTests
{
    public class AmountTests
    {
        [Theory]
        [MemberData(nameof(AmountTestData.GivenGrossAndVAT), MemberType = typeof(AmountTestData))]
        public void GivenVATAndGrossValue_shouldCalculate_CorrectNetValue(Amount given, Amount calculated)
        {
            //Arrange
            var sut = new Amount() { GrossValue = given.GrossValue, VAT = given.VAT };
            //Act
            var result = sut.CalculateNetValue();
            //Assert
            result.Should().Be(calculated.NetValue);
        }

        [Theory]
        [MemberData(nameof(AmountTestData.GivenGrossAndVAT), MemberType = typeof(AmountTestData))]
        public void GivenVATAndGrossValue_shouldCalculate_CorrectVATValue(Amount given, Amount calculated)
        {
            //Arrange
            var sut = new Amount() { GrossValue = given.GrossValue, VAT = given.VAT };
            //Act
            var result = sut.CalculateVATValue();
            //Assert
            result.Should().Be(calculated.VATValue);
        }

        [Theory]
        [MemberData(nameof(AmountTestData.GivenNetAndVAT), MemberType = typeof(AmountTestData))]
        public void GivenVATAnd—etValue_shouldCalculate_CorrectGrossValue(Amount given, Amount calculated)
        {
            //Arrange
            var sut = new Amount() { NetValue = given.NetValue, VAT = given.VAT };
            //Act
            var result = sut.CalculateGrossValue();
            //Assert
            result.Should().Be(calculated.GrossValue);
        }

        [Theory]
        [MemberData(nameof(AmountTestData.GivenNetAndVAT), MemberType = typeof(AmountTestData))]
        public void GivenVATAndNetValue_shouldCalculate_CorrectVATValue(Amount given, Amount calculated)
        {
            //Arrange
            var sut = new Amount() { NetValue = given.NetValue, VAT = given.VAT };
            //Act
            var result = sut.CalculateVATValue();
            //Assert
            result.Should().Be(calculated.VATValue);
        }

        [Theory]
        [MemberData(nameof(AmountTestData.GivenVATValueAndVAT), MemberType = typeof(AmountTestData))]
        public void GivenVATAndVATValue_shouldCalculate_CorrectGrossValue(Amount given, Amount calculated)
        {
            //Arrange
            var sut = new Amount() { VATValue = given.VATValue, VAT = given.VAT };
            //Act
            var result = sut.CalculateGrossValue();
            //Assert
            result.Should().Be(calculated.GrossValue);
        }

        [Theory]
        [MemberData(nameof(AmountTestData.GivenVATValueAndVAT), MemberType = typeof(AmountTestData))]
        public void GivenVATAndVATValue_shouldCalculate_CorrectNETValue(Amount given, Amount calculated)
        {
            //Arrange
            var sut = new Amount() { VATValue = given.VATValue, VAT = given.VAT };
            //Act
            var result = sut.CalculateNetValue();
            //Assert
            result.Should().Be(calculated.NetValue);
        }

        [Fact]
        public void GivenVATIsNull_CalculateGrossValue_ShouldReturnZero()
        {
            //Arrange 
            var sut = new Amount();
            //Act
            var result = sut.CalculateGrossValue();
            //Assert
            result.Should().Be(0);
        }

        [Fact]
        public void GivenVATIsNull_CalculateNetValue_ShouldReturnZero()
        {
            //Arrange 
            var sut = new Amount();
            //Act
            var result = sut.CalculateNetValue();
            //Assert
            result.Should().Be(0);
        }

        [Fact]
        public void GivenVATIsNull_CalculateVATValue_ShouldReturnZero()
        {
            //Arrange 
            var sut = new Amount();
            //Act
            var result = sut.CalculateVATValue();
            //Assert
            result.Should().Be(0);
        }
    }
}