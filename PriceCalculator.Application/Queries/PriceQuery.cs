using PriceCalculator.Domain.Entities;

namespace PriceCalculator.Application.Queries
{
    public record PriceQuery
    {

        /// <summary>
        /// Value before applying vat
        /// </summary>
        public string? NetValue { get; set; }

        /// <summary>
        /// Value after adding VAT value to Net Value
        /// </summary>
        public string? GrossValue { get; set; }

        /// <summary>
        /// the value difference bettween Gross and Net Values
        /// </summary>
        public string? VATValue { get; set; }

        /// <summary>
        /// VAT percentage to apply
        /// </summary>
        public string? VAT { get; set; }


        public Amount ToAmount()
        {
            var amount = new Amount();

            if (double.TryParse(NetValue, out double outValue)) { amount.NetValue = outValue; }
            if (double.TryParse(GrossValue, out outValue)) { amount.GrossValue = outValue; }
            if (double.TryParse(VATValue, out outValue)) { amount.VATValue = outValue; }
            if (double.TryParse(VAT, out outValue)) { amount.VAT = outValue; }

            return amount;
        }
    }
}
