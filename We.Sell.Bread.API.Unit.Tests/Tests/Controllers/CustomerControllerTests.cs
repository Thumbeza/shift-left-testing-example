using Microsoft.AspNetCore.Mvc;
using We.Sell.Bread.API.Unit.Tests.TestData;

namespace We.Sell.Bread.API.Unit.Tests.Tests.Controllers
{
    //only test happy paths for Controller unit tests
    //All validation and negative tests can be covered by the service tests and API integration tests
    public class CustomerControllerTests : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;
        private readonly CustomerController _controller;

        public CustomerControllerTests(BaseFixture fixture)
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

        [Fact]
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
    }


}
