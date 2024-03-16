using FluentValidation;

namespace PriceCalculator.Application.Validators
{
    public static class ValidatorExtensions
    {

        public static IRuleBuilderOptions<T, string?> ValueCanBeConvertedToDoubleAndArePositive<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            double outValue = default;
            return ruleBuilder.Must(e => Double.TryParse(e, out outValue) && outValue >= 0);
        }


        public static IRuleBuilderOptions<T, string?> InAustrianVATValues<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            string[] validAustrianVats = ["10", "20", "30"];
            return ruleBuilder.Must(e => validAustrianVats.Contains(e));
        }

        public static IRuleBuilderOptions<T, string?> NullOrEmpty<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder.Must(e => string.IsNullOrEmpty(e));
        }

        public static IRuleBuilderOptions<T, string?> AllAmountsAreNUll<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder.Must(e => string.IsNullOrEmpty(e));
        }
    }
}
