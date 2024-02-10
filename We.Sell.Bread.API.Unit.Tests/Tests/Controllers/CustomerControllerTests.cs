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

        [Fact(Skip = "Under Investigation")]
        public async Task GivenCorrectDetailsWhenCreatingCustomerReturnTypeMustBeCustomerDetailsDtoActionResults()
        {
            var name = Faker.Name.FullName();
            var phoneNumber = Faker.Phone.Number();
            var email = Faker.Internet.Email();
            var address = Faker.Address.City();

            var customer = new CustomerCommand(name, phoneNumber, email, address);

            var response = await _controller.Post(customer);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ActionResult<CustomerDto>));

            await _controller.Delete(response.Value.Id.ToString());
        }

        [Fact]
        public void GivenCorrectIdWhenRetrievingCustomerReturnTypeMustBeCustomerDetailsDtoActionResults()
        {
            var customer = _controller.Get(CustomerData.CustomerIdString);

            customer.Should().NotBeNull();
            customer.Should().BeOfType(typeof(ActionResult<CustomerDto>));
        }

        [Fact]
        public void GivenDataExistsWhenRetrievingCustomersReturnTypeMustBeListCustomerDetailsDtoActionResults()
        {
            var customer = _controller.GetAll();

            customer.Should().NotBeNull();
            customer.Should().BeOfType(typeof(ActionResult<List<CustomerDto>>));
        }

        [Fact(Skip = "Under Inverstigation")]
        public async Task GivenCorrectIdWhenDeletingCustomerReturnTypeMustBeOfNoContentResult()
        {
            var customer = await _controller.Delete(CustomerData.DeleteCustomerIdString);

            customer.Should().NotBeNull();
            customer.Should().BeOfType<NoContentResult>();
        }

        [Fact(Skip = "Under Investigation")]
        public async Task GivenInCorrectIdWhenDeletingCustomerReturnTypeMustBeOfNotFoundResult()
        {
            var customer = await _controller.Delete(CustomerData.IncorrectCustomerIdString);

            customer.Should().NotBeNull();
            customer.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GivenEmptyIdWhenDeletingCustomerReturnTypeMustBeOfBadRequestResult()
        {
            var emptyId = string.Empty;

            var customer = await _controller.Delete(emptyId);

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
            var newTestCustomerDto = new CustomerCommand(customerName, contactNo, emailAddress, physicalAddress);
            var testCustomer = (await _controller.Post(newTestCustomerDto)).Value;
            var testCustomerId = testCustomer.Id.ToString();

            newTestCustomerDto.CustomerName = "Test Customer Controller";
            newTestCustomerDto.PhysicalAddress = "Katlehong";

            var updateCustomerResponse = await _controller.Put(testCustomerId, newTestCustomerDto);

            updateCustomerResponse.Should().NotBeNull();
            updateCustomerResponse.Should().BeOfType<ActionResult<CustomerDto>>();

            await _controller.Delete(testCustomerId);
        }

        [Fact]
        public async void GivenIncorrectUserIdWhenUpdatingAnExistingCustomerResponseStatusShouldBeNotFound()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var newCustomerDetailsDto = new CustomerCommand(customerName, contactNo, emailAddress, physicalAddress);

            var incorrectId = Guid.NewGuid().ToString();
            var updateCustomerResponse = await _controller.Put(incorrectId, newCustomerDetailsDto);

            updateCustomerResponse.Should().NotBeNull();
            updateCustomerResponse.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void GivenInvalidGuidWhenUpdatingAnExistingCustomerResponseStatusShouldBeBadRequest()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var newCustomerDetailsDto = new CustomerCommand(customerName, contactNo, emailAddress, physicalAddress);

            var incorrectId = Guid.NewGuid().ToString();
            var updateCustomerResponse = await _controller.Put(incorrectId, newCustomerDetailsDto);

            updateCustomerResponse.Should().NotBeNull();
            updateCustomerResponse.Result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
