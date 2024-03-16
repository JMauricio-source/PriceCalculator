namespace PriceCalculator.Domain.Entities
{
    public class Amount
    {
        /// <summary>
        /// Value before applying vat
        /// </summary>
        public double? NetValue { get; set; }

        /// <summary>
        /// Value after adding VAT value
        /// </summary>
        public double? GrossValue { get; set; }

        /// <summary>
        /// the value difference bettween Gross and Net Values
        /// </summary>
        public double? VATValue { get; set; }

        /// <summary>
        /// VAT percentage to apply
        /// </summary>
        public double? VAT { get; set; }

        public double CalculateNetValue()
        {
            if (VAT is null) return 0;
            double result = default;
            if (GrossValue != null)
            {
                result = NetValueFromGross(GrossValue.Value, VAT.Value);
            }
            else if (VATValue != null)
            {
                result = NetValueFromVATValue(VATValue.Value, VAT.Value);
            }

            return result;
        }

        public double CalculateGrossValue()
        {
            if (VAT is null) return 0;
            double result = default;

            if (NetValue != null)
            {

                result = GrossValueFromNet(NetValue.Value, VAT.Value);
            }
            else if (VATValue != null)
            {
                result = GrossValueFromVATValue(VATValue.Value, VAT.Value);
            }

            return result;
        }

        public double CalculateVATValue()
        {
            if (VAT is null) return 0;
            double result = default;


            if (GrossValue != null)
            {
                result = VATValueFromGross(GrossValue.Value, VAT.Value);
            }
            else if (NetValue != null)
            {
                result = VATValueFromNet(NetValue.Value, VAT.Value);
            }

            return result;
        }

        //Formulas
        private static Func<double, double, double> VATValueFromGross => (gross, vat) => (gross * vat) / (100 + vat);

        private static Func<double, double, double> VATValueFromNet => (net, vat) => (net * vat) / 100;

        private static Func<double, double, double> GrossValueFromNet => (net, vat) => net + VATValueFromNet(net, vat);
        private static Func<double, double, double> GrossValueFromVATValue => (vatValue, vat) => NetValueFromVATValue(vatValue, vat) + vatValue;

        private static Func<double, double, double> NetValueFromVATValue => (vatValue, vat) => (100 * vatValue) / vat;

        private static Func<double, double, double> NetValueFromGross => (grossValue, vat) => grossValue - VATValueFromGross(grossValue, vat);


    }
}
