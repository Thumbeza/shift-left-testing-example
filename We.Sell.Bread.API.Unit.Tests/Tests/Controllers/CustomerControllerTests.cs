using Microsoft.AspNetCore.Mvc;
using We.Sell.Bread.API.Unit.Tests.TestData;

namespace We.Sell.Bread.API.Unit.Tests.Tests.Controllers
{
    //only test happy paths for Controller unit tests
    //All validation and negative tests can be covered by the service tests and API integration tests
    public class CustomerControllerTests : IClassFixture<BaseFixture<CustomerController>>
    {
        private readonly BaseFixture<CustomerController> _fixture;
        private readonly CustomerController _controller;

        public CustomerControllerTests(BaseFixture<CustomerController> fixture)
        {
            _fixture = fixture;

            _controller = new CustomerController(_fixture.Logger);
        }

        [Fact(Skip = "Under Inverstigation")]
        public async Task GivenCorrectDetailsWhenCreatingCustomerReturnTypeMustBeCustomerDetailsDtoActionRetusults()
        {
            var name = Faker.Name.FullName();
            var phuneNumber = Faker.Phone.Number();
            var email = Faker.Internet.Email();
            var address = Faker.Address.City();

            var customer = new NewCustomerDto(name, phuneNumber, email, address);

            var response = await _controller.PostAsync(customer);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ActionResult<CustomerDetailsDto>));

            await _controller.DeleteCustomerAsync(response.Value.Id.ToString());
        }

        [Fact]
        public void GivenCorrectIdWhenRetrievingCustomerReturnTypeMustBeCustomerDetailsDtoActionResults()
        {
            var customer = _controller.GetById(CustomerData.CustomerIdString);

            customer.Should().NotBeNull();
            customer.Should().BeOfType(typeof(ActionResult<CustomerDetailsDto>));
        }

        [Fact]
        public void GivenDataExistsWhenRetrievingCustomersReturnTypeMustBeListCustomerDetailsDtoActionResults()
        {
            var customer = _controller.GetAll();

            customer.Should().NotBeNull();
            customer.Should().BeOfType(typeof(ActionResult<List<CustomerDetailsDto>>));
        }

        [Fact(Skip = "Under Inverstigation")]
        public async Task GivenCorrectIdWhenDeletingCustomerReturnTypeMustBeOfNoContentResult()
        {
            var customer = await _controller.DeleteCustomerAsync(CustomerData.DeleteCustomerIdString);

            customer.Should().NotBeNull();
            customer.Should().BeOfType<NoContentResult>();
        }

        [Fact(Skip = "Under Inverstigation")]
        public async Task GivenInCorrectIdWhenDeletingCustomerReturnTypeMustBeOfNotFoundResult()
        {
            var customer = await _controller.DeleteCustomerAsync(CustomerData.IncorrectCustomerIdString);

            customer.Should().NotBeNull();
            customer.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GivenEmptyIdWhenDeletingCustomerReturnTypeMustBeOfBadRequestResult()
        {
            var emptyId = string.Empty;

            var customer = await _controller.DeleteCustomerAsync(emptyId);

            customer.Should().NotBeNull();
            customer.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void GivenCorrectDetailsWhenUpdatingAnExistingCustomerResponseStatusShouldBeCustomerDetailsDtoActionResult()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();
            var newTestCustomerDto = new NewCustomerDto(customerName, contactNo, emailAddress, physicalAddress);
            var testCustomer = (await _controller.PostAsync(newTestCustomerDto)).Value;
            var testCustomerId = testCustomer.Id.ToString();

            newTestCustomerDto.CustomerName = "Test Customer Controller";
            newTestCustomerDto.PhysicalAddress = "Katlehong";

            var updateCustomerResponse = await _controller.Put(testCustomerId, newTestCustomerDto);

            updateCustomerResponse.Should().NotBeNull();
            updateCustomerResponse.Should().BeOfType<ActionResult<CustomerDetailsDto>>();

            await _controller.DeleteCustomerAsync(testCustomerId);
        }

        [Fact]
        public async void GivenIncorrectUserIdWhenUpdatingAnExistingCustomerResponseStatusShouldBeNotFound()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var newCustomerDetailsDto = new NewCustomerDto(customerName, contactNo, emailAddress, physicalAddress);

            var incorrectId = Guid.NewGuid().ToString();
            var updateCustomerResponse = await _controller.Put(incorrectId, newCustomerDetailsDto);

            updateCustomerResponse.Should().NotBeNull();
            updateCustomerResponse.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async void GivenInvalidGuidWhenUpdatingAnExistingCustomerResponseStatusShouldBeBadRequest()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var newCustomerDetailsDto = new NewCustomerDto(customerName, contactNo, emailAddress, physicalAddress);

            var incorrectId = Guid.NewGuid().ToString();
            var updateCustomerResponse = await _controller.Put(incorrectId, newCustomerDetailsDto);

            updateCustomerResponse.Should().NotBeNull();
            updateCustomerResponse.Result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
