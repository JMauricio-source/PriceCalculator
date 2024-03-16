using PriceCalculator.Domain.Entities;

namespace PriceCalculator.Application
{
    public class PriceCalculatorService
    {

        public Amount CalculateAmount(Amount amount)
        {
            amount.GrossValue ??= amount.CalculateGrossValue();
            amount.NetValue ??= amount.CalculateNetValue();
            amount.VATValue ??= amount.CalculateVATValue();

            return amount;
        }
    }
}
