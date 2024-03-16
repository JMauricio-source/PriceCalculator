using PriceCalculator.Application.Queries;
using PriceCalculator.Domain.Entities;

namespace PriceCalculator.UnitTests.TestData
{
    public static class PriceQueryTestData
    {
        public static TheoryData<PriceQuery, Amount> AllObjectStates =>
        new()
        {
            {
                new()
                {
                    GrossValue = "100",
                    VAT = "30"
                },
                new() { GrossValue = 100, VAT = 30}
            },{
                new()
                {
                    NetValue = "100",
                    VAT = "30"
                },
                new() { NetValue = 100, VAT = 30}
            }
            ,{
                new()
                {
                    VATValue = "30",
                     VAT = "30"
                },
                new() { VATValue = 30, VAT = 30}
            }
            ,{
                new()
                {
                    GrossValue = "130",
                    NetValue = "100",
                    VATValue = "30",
                    VAT = "30",
                },
                new() { GrossValue = 130,NetValue = 100,VATValue = 30, VAT = 30}
            }


        };
    }
}
