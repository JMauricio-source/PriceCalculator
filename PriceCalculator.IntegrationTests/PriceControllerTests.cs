using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using PriceCalculator.Application.Queries;
using PriceCalculator.Application.Validators;
using PriceCalculator.Domain.Entities;
using PriceCalculator.IntegrationTests.TestData;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PriceCalculator.IntegrationTests
{
    public class PriceControllerTests : IClassFixture<WebApplicationFactory<PriceCalculator.UI.Program>>
    {
        protected readonly WebApplicationFactory<PriceCalculator.UI.Program> _factory;
        protected readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;
        public PriceControllerTests(WebApplicationFactory<PriceCalculator.UI.Program> fixture)
        {
            _factory = fixture;
            _client = _factory.CreateClient();
            _jsonOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        }

        [Theory]
        [MemberData(nameof(PriceControllerTestData.CorrectInputs), MemberType = typeof(PriceControllerTestData))]
        public async Task CallingPost_WithValidPayload_ShouldReturn200OK(PriceQuery query, Amount amount)
        {
            //Arrange 
            var jsonQuery = JsonSerializer.Serialize(query);
            var content = new StringContent(jsonQuery, MediaTypeHeaderValue.Parse("application/json"));

            //Act
            var response = await _client.PostAsync("/price", content);

            //Assert
            var responseJsonObject = await response.Content.ReadAsStringAsync();

            var amountObject = JsonSerializer.Deserialize<Amount>(responseJsonObject, _jsonOptions);

            response.EnsureSuccessStatusCode();

            amountObject?.VAT.Should().Be(amount.VAT);
            amountObject?.NetValue.Should().Be(amount.NetValue);
            amountObject?.GrossValue.Should().Be(amount.GrossValue);
            amountObject?.VATValue.Should().Be(amount.VATValue);

        }

        [Theory]
        [MemberData(nameof(PriceControllerTestData.IncorrectInputs), MemberType = typeof(PriceControllerTestData))]
        public async Task CallingPost_WithInValidPayload_ShouldReturn404BadRequest(PriceQuery query, string message)
        {
            //Arrange 
            var jsonQuery = JsonSerializer.Serialize(query);
            var content = new StringContent(jsonQuery, MediaTypeHeaderValue.Parse("application/json"));
            var allowedMessages = new List<string>() { ValidatorConstants.MissingOrInvalidVATRate, ValidatorConstants.MissingOrInvalidAmount, ValidatorConstants.MoreThanOneInput };

            //Act
            var response = await _client.PostAsync("/price", content);

            //Assert
            var responseJsonObject = await response.Content.ReadAsStringAsync();
            var responses = JsonSerializer.Deserialize<IEnumerable<ProblemDetails>>(responseJsonObject, _jsonOptions);
            var errorMessages = responses?.Select(e => e.Extensions["errorMessage"]?.ToString()).Distinct().ToList();

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            errorMessages?.ForEach(e => allowedMessages.Contains(e ?? ""));
            errorMessages?.Contains(message);

        }

        [Theory]
        [MemberData(nameof(PriceControllerTestData.CorrectInputs), MemberType = typeof(PriceControllerTestData))]
        public async Task CallingGet_WithValidPayload_ShouldReturn200OK(PriceQuery query, Amount amount)
        {
            //Arrange 
            var urlQuery = PriceQueryToQueryString(query);

            //Act
            var response = await _client.GetAsync($"/price?{urlQuery}");

            //Assert
            var responseJsonObject = await response.Content.ReadAsStringAsync();
            var amountObject = JsonSerializer.Deserialize<Amount>(responseJsonObject, _jsonOptions);

            response.EnsureSuccessStatusCode();

            amountObject?.VAT.Should().Be(amount.VAT);
            amountObject?.NetValue.Should().Be(amount.NetValue);
            amountObject?.GrossValue.Should().Be(amount.GrossValue);
            amountObject?.VATValue.Should().Be(amount.VATValue);

        }

        [Theory]
        [MemberData(nameof(PriceControllerTestData.IncorrectInputs), MemberType = typeof(PriceControllerTestData))]
        public async Task CallingGet_WithInValidPayload_ShouldReturn404BadRequest(PriceQuery query, string message)
        {
            //Arrange 
            //Arrange 
            var urlQuery = PriceQueryToQueryString(query);
            var allowedMessages = new List<string>() { ValidatorConstants.MissingOrInvalidVATRate, ValidatorConstants.MissingOrInvalidAmount, ValidatorConstants.MoreThanOneInput };

            //Act
            var response = await _client.GetAsync($"/price?{urlQuery}");

            //Assert
            var responseJsonObject = await response.Content.ReadAsStringAsync();
            var responses = JsonSerializer.Deserialize<IEnumerable<ProblemDetails>>(responseJsonObject, _jsonOptions);
            var errorMessages = responses?.Select(e => e.Extensions["errorMessage"]?.ToString()).Distinct().ToList();

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            errorMessages?.ForEach(e => allowedMessages.Contains(e ?? ""));
            errorMessages?.Contains(message);

        }

        private static string PriceQueryToQueryString(PriceQuery? query)
        {
            StringBuilder sb = new();
            if (!string.IsNullOrEmpty(query?.GrossValue))
            {
                sb.Append($"grossvalue={query?.GrossValue}&");
            }
            if (!string.IsNullOrEmpty(query?.NetValue))
            {
                sb.Append($"netvalue={query?.NetValue}&");
            }
            if (!string.IsNullOrEmpty(query?.VATValue))
            {
                sb.Append($"vatvalue={query?.VATValue}&");
            }
            if (!string.IsNullOrEmpty(query?.VAT))
            {
                sb.Append($"vat={query?.VAT}");
            }

            return sb.ToString();
        }

    }
}
