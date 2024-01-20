using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace We.Sell.Bread.API.Integration.Tests.Tests
{
    public class CustomerControllerTests
    {
        private const string _baseUrl = "https://localhost";
        private readonly HttpClient _httpClient;
        public CustomerControllerTests() 
        {
            var webApplicationFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => 
                {
                    builder.ConfigureServices(services => { });
                });

            _httpClient = webApplicationFactory.CreateClient(new() {BaseAddress = new Uri(_baseUrl)});
        }

        [Fact]
        public async Task GivenCorrectCustomerIdWhenRetrievingCustomerReturnOkStatusCodeAndCustomerDetails()
        {
            var customerId = "ad3d2d6c-02fd-48c9-a287-fa33aa197053";

            var response = await _httpClient.GetAsync($"/Customer/GetById/{customerId}");


            //response.IsSuccessStatusCode.Should().BeTrue();
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
        public void GivenCorrectCustomerIdWhenCreatingCustomerReturnOStatusCode()
        {
            //using var application = new WebApplicationFactory<Program>();
            //using var client = application.CreateClient();

            //var name = Faker.Name.FullName();
            //var phuneNumber = Faker.Phone.Number();
            //var email = Faker.Internet.Email();
            //var address = Faker.Address.City();

            //var requestContent = new NewCustomerDto(name, phuneNumber, email, address);

            //var response = client.PostAsync("/Customer/Post", JsonHelper.Serialize(requestContent));

            //response.StatusCode.Should().Be(HttpStatusCode.OK);
            //response.Content.Should().Be($"Customer Id: '{customerId}' is not a valid Guid.");
        }

       
    }
}
