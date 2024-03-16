using FluentAssertions;
using PriceCalculator.Application.Queries;
using PriceCalculator.Application.Validators;
using PriceCalculator.UnitTests.TestData;

namespace PriceCalculator.UnitTests
{
    public class PriceQueryValidatorTests
    {

        [Fact]
        public void PriceQueryWithoutVAT_shouldReturn_ProblemDetailWithCorrectMessage()
        {
            //Arrange
            var query = new PriceQuery();
            var sut = new PriceQueryValidator();

            //Act
            var validationResult = sut.Validate(query);

            //Assert
            validationResult.Errors.Count().Should().Be(2);
            validationResult.Errors[0].ErrorMessage.Should().Be(ValidatorConstants.MissingOrInvalidVATRate);
        }

        [Fact]
        public void PriceQueryWithNonAustrianVat_shouldReturn_ProblemDetailWithCorrectMessage()
        {
            //Arrange
            var query = new PriceQuery() { VAT = "40" };
            var sut = new PriceQueryValidator();

            //Act
            var validationResult = sut.Validate(query);

            //Assert
            _ = validationResult.Errors.Count.Should().Be(2);
            validationResult.Errors[0].ErrorMessage.Should().Be(ValidatorConstants.MissingOrInvalidVATRate);
        }

        [Fact]
        public void PriceQueryWithVATAndMoreThanOneAmount_shouldReturn_ProblemDetailWithCorrectMessage()
        {
            //Arrange
            var query = new PriceQuery() { VAT = "10", GrossValue = "110", NetValue = "100", VATValue = "10" };
            var sut = new PriceQueryValidator();

            //Act
            var validationResult = sut.Validate(query);

            //Assert
            _ = validationResult.Errors.Count.Should().Be(3);
            validationResult.Errors[0].ErrorMessage.Should().Be(ValidatorConstants.MoreThanOneInput);
            validationResult.Errors[0].PropertyName.Should().Be("GrossValue");
            validationResult.Errors[1].ErrorMessage.Should().Be(ValidatorConstants.MoreThanOneInput);
            validationResult.Errors[1].PropertyName.Should().Be("NetValue");
            validationResult.Errors[2].ErrorMessage.Should().Be(ValidatorConstants.MoreThanOneInput);
            validationResult.Errors[2].PropertyName.Should().Be("VATValue");
        }

        [Theory]
        [MemberData(nameof(PriceQueryValidatorTestData.InvalidAmounts), MemberType = typeof(PriceQueryValidatorTestData))]
        public void PriceQueryWithVATAndNegativeORInvalidAmount_shouldReturn_ProblemDetailWithCorrectMessage(PriceQuery query, string message)
        {
            //Arrange

            var sut = new PriceQueryValidator();

            //Act
            var validationResult = sut.Validate(query);

            //Assert
            _ = validationResult.Errors.Count.Should().Be(1);
            validationResult.Errors[0].ErrorMessage.Should().Be(message);
        }
    }
}
