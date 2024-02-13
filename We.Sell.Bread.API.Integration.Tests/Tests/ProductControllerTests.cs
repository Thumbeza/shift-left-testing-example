using We.Sell.Bread.API.Integration.Tests.Helpers;
using We.Sell.Bread.API.Integration.Tests.TestData;
using We.Sell.Bread.Core.DTOs.Product;

namespace We.Sell.Bread.API.Integration.Tests.Tests
{
    public class ProductControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient = factory.CreateClient();

        private static string InvalidGuid => "3fa85f64";

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
            var response = await _httpClient.GetAsync($"/Product/Get/{Product.Id}");

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GivenCorrectProductIdWhenRetrievingProductReturnProductDtoCodeAsync()
        {
            var productId = Product.Id;

            var response = await _httpClient.GetAsync($"/Product/Get/{productId}");

            response.IsSuccessStatusCode.Should().BeTrue();

            var responseContent = await response.Content.ReadAsStringAsync();
            var product = JsonUtils.DeserializeToDto<ProductDto>(responseContent);

            product.Id.Should().Be(new Guid(productId));
        }

        [Fact]
        public async Task GivenInCorrectProductIdWhenRetrievingProductReturnNotFoundStatusCodeAsync()
        {
            var incorrectProductId = Guid.NewGuid().ToString();

            var response = await _httpClient.GetAsync($"/Product/Get/{incorrectProductId}");

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GivenInvalidProductIdWhenRetrievingProductReturnBadRequestStatusCodeAsync()
        {
            var response = await _httpClient.GetAsync($"/Product/Get/{InvalidGuid}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be($"Product Id: '{InvalidGuid}' is not a valid Guid.");
        }

        [Fact]
        public async Task GivenDataExistWhenRetrievingAllProductReturnOkStatusCodeAsync()
        {
            var response = await _httpClient.GetAsync("/Product/GetAll");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
