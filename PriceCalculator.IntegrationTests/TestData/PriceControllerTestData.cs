using PriceCalculator.Application.Queries;
using PriceCalculator.Application.Validators;
using PriceCalculator.Domain.Entities;

namespace PriceCalculator.IntegrationTests.TestData
{
    public static class PriceControllerTestData
    {
        public static TheoryData<PriceQuery, Amount> CorrectInputs =>
        new()
        {
            {
                new()
                {
                    GrossValue = "110",
                    VAT = "10"
                },
                new() { NetValue=100,GrossValue = 110, VATValue=10, VAT = 10}
            }
            ,{
                new()
                {
                    NetValue = "100",
                    VAT = "10"
                },
                new() { NetValue=100,GrossValue = 110, VATValue=10, VAT = 10}
            }
            ,{
                new()
                {
                    VATValue = "10",
                     VAT = "10"
                },
                new() { NetValue=100,GrossValue = 110, VATValue=10, VAT = 10}
            }
        };

        public static TheoryData<PriceQuery, string> IncorrectInputs =>
        new()
        {
            {
                new()
                {
                    GrossValue = "-110",
                    VAT = "10"
                },
                ValidatorConstants.MissingOrInvalidAmount
            }
            ,{
                new()
                {
                    NetValue = "-100",
                    VAT = "10"
                },
                ValidatorConstants.MissingOrInvalidAmount
            }
            ,{
                new()
                {
                    VATValue = "-10",
                     VAT = "10"
                },
                ValidatorConstants.MissingOrInvalidAmount
            },
            {
                new()
                {
                    VAT = "10"
                },
                ValidatorConstants.MissingOrInvalidAmount
            }
            ,{
                new()
                {
                    GrossValue = "110",
                    NetValue = "100",
                    VATValue = "10",

                },
                ValidatorConstants.MoreThanOneInput
            }
            ,{
                new()
                {
                   GrossValue = "110",
                },
               ValidatorConstants.MissingOrInvalidVATRate
            }
        };

    }
}
