using FluentAssertions;
using PriceCalculator.Application.Queries;
using PriceCalculator.Domain.Entities;
using PriceCalculator.UnitTests.TestData;

namespace PriceCalculator.UnitTests
{
    public class PriceQueryTests
    {

        [Theory]
        [MemberData(nameof(PriceQueryTestData.AllObjectStates), MemberType = typeof(PriceQueryTestData))]
        public void GivenVATAndGrossValue_shouldCalculate_CorrectVATValue(PriceQuery given, Amount calculated)
        {

            //Act
            var result = given.ToAmount();
            //Assert
            result.VAT.Should().Be(calculated.VAT);
            result.VATValue.Should().Be(calculated.VATValue);
            result.GrossValue.Should().Be(calculated.GrossValue);
            result.NetValue.Should().Be(calculated.NetValue);
        }
    }
}
