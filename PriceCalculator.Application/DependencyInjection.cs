using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PriceCalculator.Application.Queries;
using PriceCalculator.Application.Validators;

namespace PriceCalculator.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<PriceCalculatorService>();


            services.AddScoped<IValidator<PriceQuery>, PriceQueryValidator>();


            return services;
        }
    }


}
