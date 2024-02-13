using We.Sell.Bread.API.Integration.Tests.Helpers;
using We.Sell.Bread.API.Integration.Tests.TestData;
using We.Sell.Bread.Core.DTOs.Customer;

namespace We.Sell.Bread.API.Integration.Tests.Tests
{
    public class CustomerControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient = factory.CreateClient();

        private static string InvalidGuid => "3fa85f64";

        [Fact]
        public async Task GivenCustomerControllerWhenCheckingForServerHealthReturnOkStatusAsync()
        {
            var response = await _httpClient.GetAsync($"/Customer/HealthCheck");

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GivenCorrectCustomerIdWhenRetrievingCustomerReturnOkStatusCodeAsync()
        {
            var response = await _httpClient.GetAsync($"/Customer/Get/{Customer.Id}");

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GivenCorrectCustomerIdWhenRetrievingCustomerReturnCustomerDtoCodeAsync()
        {
            var customerId = Customer.Id;

            var response = await _httpClient.GetAsync($"/Customer/Get/{customerId}");

            response.IsSuccessStatusCode.Should().BeTrue();

            var responseContent = await response.Content.ReadAsStringAsync();
            var customer = JsonUtils.DeserializeToDto<CustomerDto>(responseContent);

            customer.Id.Should().Be(new Guid(customerId));
        }

        [Fact]
        public async Task GivenInCorrectCustomerIdWhenRetrievingCustomerReturnNotFoundStatusCodeAsync()
        {
            var customerId = Guid.NewGuid().ToString();

            var response = await _httpClient.GetAsync($"/Customer/Get/{customerId}");

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GivenInvalidCustomerIdWhenRetrievingCustomerReturnBadRequestStatusCodeAsync()
        {
            var response = await _httpClient.GetAsync($"/Customer/Get/{InvalidGuid}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be($"Customer Id: '{InvalidGuid}' is not a valid Guid.");
        }

        [Fact]
        public async Task GivenInCorrectCustomerIdWhenDeletingCustomerReturnBadRequestStatusCodeAsync()
        {
            var customerId = Guid.NewGuid().ToString();

            var response = await _httpClient.DeleteAsync($"/Customer/Delete/{customerId}");

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GivenInvalidCustomerIdWhenDeletingCustomerReturnBadRequestStatusCodeAsync()
        {
            var response = await _httpClient.DeleteAsync($"/Customer/Delete/{InvalidGuid}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be($"Customer Id: '{InvalidGuid}' is not a valid Guid.");
        }

        [Fact]
        public async Task GivenInCorrectCustomerIdWhenUpdatingCustomerReturnBadRequestStatusCodeAsync()
        {
            var customerId = Guid.NewGuid().ToString();
            var customerName = Faker.Name.FullName();

            var updateCommand = CustomerCommand(customerName);

            var response = await _httpClient.PutAsJsonAsync($"/Customer/Put/{customerId}", updateCommand);

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GivenInvalidCustomerIdWhenUpdatingCustomerReturnBadRequestStatusCodeAsync()
        {
            var customerName = Faker.Name.FullName();

            var updateCommand = CustomerCommand(customerName);

            var response = await _httpClient.PutAsJsonAsync($"/Customer/Put/{InvalidGuid}", updateCommand);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be($"Customer Id: '{InvalidGuid}' is not a valid Guid.");
        }

        [Fact]
        public async Task GivenEmptyNameWhenUpdatingCustomerReturnBadRequestStatusCodeAsync()
        {
            var updateCommand = CustomerCommand();

            var response = await _httpClient.PutAsJsonAsync($"/Customer/Put/{Customer.Id}", updateCommand);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be("One or more customer details were invalid");
        }

        [Fact]
        public async Task GivenEmptyNameWhenCreatingCustomerReturnInternalServerErrorAsync()
        {
            var updateCommand = CustomerCommand();

            var response = await _httpClient.PostAsJsonAsync($"/Customer/Post", updateCommand);

            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task GivenDataExistWhenRetrievingAllCustomerReturnOkStatusCodeAsync()
        {
            var response = await _httpClient.GetAsync("/Customer/Get/All");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CRUDTestAsync()
        {
            //create
            var customerName = Faker.Name.FullName();
            var createCommand = CustomerCommand(customerName);

            var response = await _httpClient.PostAsJsonAsync($"/Customer/Post", createCommand);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync();
            var customer = JsonUtils.DeserializeToDto<CustomerDto>(responseContent);
            customer.CustomerName.Should().Be(customerName);

            //read
            var getResponse = await _httpClient.GetAsync($"/Customer/Get/{customer.Id}");

            getResponse.IsSuccessStatusCode.Should().BeTrue();
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getResponseContent = await getResponse.Content.ReadAsStringAsync();
            var getCustomer = JsonUtils.DeserializeToDto<CustomerDto>(getResponseContent);
            getCustomer.CustomerName.Should().Be(customerName);

            //update
            var updatedCustomerName = Faker.Name.FullName();
            var updateCommand = CustomerCommand(updatedCustomerName);

            var updateResponse = await _httpClient.PutAsJsonAsync($"/Customer/Put/{customer.Id}", updateCommand);
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var updateResponseContent = await updateResponse.Content.ReadAsStringAsync();
            var updateCustomer = JsonUtils.DeserializeToDto<CustomerDto>(updateResponseContent);
            updateCustomer.CustomerName.Should().Be(updatedCustomerName);

            //delete
            var deleteResponse = await _httpClient.DeleteAsync($"/Customer/Delete/{customer.Id}");

            deleteResponse.IsSuccessStatusCode.Should().BeTrue();
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        }

        private static CustomerCommand CustomerCommand(string? customerName = null)
        {
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            return new CustomerCommand(customerName ?? string.Empty, contactNo, emailAddress, physicalAddress);
        }
    }
}
