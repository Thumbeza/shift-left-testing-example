using We.Sell.Bread.API.Integration.Tests.Helpers;
using We.Sell.Bread.Core.DTOs.Product;

namespace We.Sell.Bread.API.Integration.Tests.Tests
{
    public class ProductControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient = factory.CreateClient();

        [Fact]
        public async Task GivenProductControllerWhenCheckingForServerHealthReturnOkStatusAsync()
        {
            var response = await _httpClient.GetAsync($"/Product/HealthCheck");

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GivenCorrectProductIdWhenRetrievingProductReturnOkStatusCodeAsync()
        {
            var productId = "7782eb61-534e-40ba-aa2e-6f6e54ae2bfc";

            var response = await _httpClient.GetAsync($"/Product/Get/{productId}");

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GivenCorrectProductIdWhenRetrievingProductReturnProductDtoCodeAsync()
        {
            var productId = "7782eb61-534e-40ba-aa2e-6f6e54ae2bfc";

            var response = await _httpClient.GetAsync($"/Product/Get/{productId}");

            response.IsSuccessStatusCode.Should().BeTrue();

            var responseContent = await response.Content.ReadAsStringAsync();
            var product = JsonUtils.DeserializeToDto<ProductDto>(responseContent);

            product.Id.Should().Be(new Guid(productId));
        }

        [Fact]
        public async Task GivenInCorrectProductIdWhenRetrievingProductReturnNotFoundStatusCodeAsync()
        {
            var incorrectProductId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";

            var response = await _httpClient.GetAsync($"/Product/Get/{incorrectProductId}");

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GivenInvalidProductIdWhenRetrievingProductReturnBadRequestStatusCodeAsync()
        {
            var productId = "3fa85f64";

            var response = await _httpClient.GetAsync($"/Product/Get/{productId}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be($"Product Id: '{productId}' is not a valid Guid.");
        }

        [Fact]
        public async Task GivenDataExistWhenRetrievingAllProductReturnOkStatusCodeAsync()
        {
            var response = await _httpClient.GetAsync("/Product/GetAll");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
