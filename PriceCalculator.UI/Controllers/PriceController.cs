using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PriceCalculator.Application;
using PriceCalculator.Application.Queries;
using PriceCalculator.Domain.Entities;
using System.Net.Mime;

namespace PriceCalculator.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController(IValidator<PriceQuery> validator, PriceCalculatorService priceCalculatorService, ILogger<PriceController> logger) : ControllerBase
    {
        private readonly IValidator<PriceQuery> _validator = validator;
        private readonly ILogger<PriceController> _logger = logger;

        private readonly PriceCalculatorService _priceCalculatorService = priceCalculatorService;

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Amount> Post([FromBody] PriceQuery amount)
        {
            var validationResult = _validator.Validate(amount);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Invalid input on {Method} request: {input}", "POST", amount);
                return BadRequest(validationResult.Errors);
            }
            var result = _priceCalculatorService.CalculateAmount(amount.ToAmount());


            return result;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Amount> Get([FromQuery] string? grossValue, [FromQuery] string? netValue, [FromQuery] string? vatValue, [FromQuery] string? vat)
        {
            PriceQuery amount = new()
            {
                GrossValue = grossValue,
                NetValue = netValue,
                VATValue = vatValue,
                VAT = vat
            };

            var validationResult = _validator.Validate(amount);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Invalid input on {Method} request: {input}", "GET", amount);
                return BadRequest(validationResult.Errors);
            }
            var result = _priceCalculatorService.CalculateAmount(amount.ToAmount());


            return result;
        }
    }
}
