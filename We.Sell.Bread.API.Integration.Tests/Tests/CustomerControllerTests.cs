using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace We.Sell.Bread.API.Integration.Tests.Tests
{
    public class CustomerControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public CustomerControllerTests(
            WebApplicationFactory<Program> factory)
        {
            _factory = factory;

            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task GivenCorrectCustomerIdWhenRetrievingCustomerReturnOkStatusCodeAndCustomerDetails()
        {
            var customerId = "ad3d2d6c-02fd-48c9-a287-fa33aa197053";

            var response = await _httpClient.GetAsync($"/Customer/GetById/{customerId}");


            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadAsStringAsync();
        }

        [Fact]
        public async Task GivenInCorrectCustomerIdWhenRetrievingCustomerReturnNotFoundStatusCodeAndCustomerDetails()
        {
            var incorrectCustomerId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";

            var response = await _httpClient.GetAsync($"/Customer/GetById/{incorrectCustomerId}");

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GivenInvalidCustomerIdWhenRetrievingCustomerReturnBadRequestStatusCodeAsync()
        {
            var customerId = "3fa85f64";

            var response = await _httpClient.GetAsync($"/Customer/GetById/{customerId}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Should().Be($"Customer Id: '{customerId}' is not a valid Guid.");
        }

        [Fact]
        public async Task GivenDataExistWhenRetrievingAllCustomerReturnOkStatusCodeAsync()
        {
            var response = await _httpClient.GetAsync("/Customer/GetAllCustomers");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

       
    }
}
