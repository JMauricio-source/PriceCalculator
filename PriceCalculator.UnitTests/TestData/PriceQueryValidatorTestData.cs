using PriceCalculator.Application.Queries;
using PriceCalculator.Application.Validators;

namespace PriceCalculator.UnitTests.TestData
{
    public static class PriceQueryValidatorTestData
    {

        public static TheoryData<PriceQuery, string> InvalidAmounts =>
         new()
        {
            {
                new()
                {
                    VAT = "30"
                },
                ValidatorConstants.MissingOrInvalidAmount
            },{
                new()
                {
                    GrossValue = "-100",
                    VAT = "30"
                },
                ValidatorConstants.MissingOrInvalidAmount
            },{
                new()
                {
                    NetValue = "-100",
                    VAT = "30"
                },
                 ValidatorConstants.MissingOrInvalidAmount
            },{
                new()
                {
                    VATValue = "-100",
                    VAT = "30"
                },
                 ValidatorConstants.MissingOrInvalidAmount
            },{
                new()
                {
                    GrossValue = "not_a_double",
                    VAT = "30"
                },
                ValidatorConstants.MissingOrInvalidAmount
            },{
                new()
                {
                    NetValue = "not_a_double",
                    VAT = "30"
                },
                 ValidatorConstants.MissingOrInvalidAmount
            },{
                new()
                {
                    VATValue = "not_a_double",
                    VAT = "30"
                },
                 ValidatorConstants.MissingOrInvalidAmount
            },{
                new()
                {
                    VATValue = "10",
                    VAT = "not_a_double"
                },
                 ValidatorConstants.MissingOrInvalidVATRate
            }


        };
    }
}
