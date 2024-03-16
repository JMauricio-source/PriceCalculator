using PriceCalculator.Domain.Entities;

namespace PriceCalculator.UnitTests.TestData
{
    public static class AmountTestData
    {

        public static TheoryData<Amount, Amount> GivenGrossAndVAT =>
        new()
        {
            {
                new()
                {
                    GrossValue = 110,
                    VAT = 10,
                },
                new() { NetValue = 100, VATValue = 10}
            },{
                new()
                {
                    GrossValue = 120,
                    VAT = 20,
                },
                new() { NetValue = 100, VATValue = 20}
            }
            ,{
                new()
                {
                    GrossValue = 130,
                    VAT = 30,
                },
                new() { NetValue = 100, VATValue = 30}
            }

        };

        public static TheoryData<Amount, Amount> GivenNetAndVAT =>
       new()
       {
            {
                new()
                {
                    NetValue = 100,
                    VAT = 10,
                },
                new() { GrossValue = 110, VATValue = 10}
            },{
                new()
                {
                    NetValue = 100,
                    VAT = 20,
                },
                new() { GrossValue = 120, VATValue = 20}
            }
            ,{
                new()
                {
                    NetValue = 100,
                    VAT = 30,
                },
                new() { GrossValue = 130, VATValue = 30}
            }

       };

        public static TheoryData<Amount, Amount> GivenVATValueAndVAT =>
       new()
       {
            {
                new()
                {
                    VATValue = 10,
                    VAT = 10,
                },
                new() { GrossValue = 110, NetValue = 100}
            },{
                new()
                {
                    VATValue = 20,
                    VAT = 20,
                },
                new() { GrossValue = 120, NetValue = 100}
            }
            ,{
                new()
                {
                    VATValue = 30,
                    VAT = 30,
                },
                new() { GrossValue = 130, NetValue = 100}
            }

       };
    }
}
