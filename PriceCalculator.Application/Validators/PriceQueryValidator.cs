using FluentValidation;
using PriceCalculator.Application.Queries;

namespace PriceCalculator.Application.Validators
{
    public class PriceQueryValidator : AbstractValidator<PriceQuery>
    {
        public PriceQueryValidator()
        {

            RuleFor(price => price.VAT)
                .InAustrianVATValues()
                .WithMessage(ValidatorConstants.MissingOrInvalidVATRate);


            RuleFor(price => price.GrossValue)
                .NullOrEmpty()
                .When(e => !String.IsNullOrEmpty(e.NetValue) || !String.IsNullOrEmpty(e.VATValue))
                .WithMessage(ValidatorConstants.MoreThanOneInput);

            RuleFor(price => price.NetValue)
               .NullOrEmpty()
               .When(e => !String.IsNullOrEmpty(e.GrossValue) || !String.IsNullOrEmpty(e.VATValue))
               .WithMessage(ValidatorConstants.MoreThanOneInput);

            RuleFor(price => price.VATValue)
               .NullOrEmpty()
               .When(e => !String.IsNullOrEmpty(e.GrossValue) || !String.IsNullOrEmpty(e.NetValue))
               .WithMessage(ValidatorConstants.MoreThanOneInput);

            RuleFor(e => e)
                .Must(e => !String.IsNullOrEmpty(e.GrossValue) || !String.IsNullOrEmpty(e.NetValue) || !String.IsNullOrEmpty(e.VATValue))
                .WithMessage(ValidatorConstants.MissingOrInvalidAmount);

            RuleFor(price => price.GrossValue).ValueCanBeConvertedToDoubleAndArePositive().When(price => !string.IsNullOrEmpty(price.GrossValue))
               .WithMessage(ValidatorConstants.MissingOrInvalidAmount);

            RuleFor(price => price.NetValue).ValueCanBeConvertedToDoubleAndArePositive().When(price => !string.IsNullOrEmpty(price.NetValue))
                .WithMessage(ValidatorConstants.MissingOrInvalidAmount);

            RuleFor(price => price.VATValue).ValueCanBeConvertedToDoubleAndArePositive().When(price => !string.IsNullOrEmpty(price.VATValue))
                .WithMessage(ValidatorConstants.MissingOrInvalidAmount);


        }
    }
}
