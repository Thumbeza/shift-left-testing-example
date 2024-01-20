﻿using Microsoft.AspNetCore.Mvc;
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

        [Fact]
        public void GivenCorrectDetailsWhenCreatingCustomerReturnTypeMustBeCustomerDetailsDtoActionRetusults()
        {
            var name = Faker.Name.FullName();
            var phuneNumber = Faker.Phone.Number();
            var email = Faker.Internet.Email();
            var address = Faker.Address.City();

            var customer = new NewCustomerDto(name, phuneNumber, email, address);

            var response = _controller.Post(customer);

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

        [Fact]
        public void GivenCorrectIdWhenDeletingCustomerReturnTypeMustBeOfNoContentObjectResult()
        {
            var customerId = Guid.NewGuid().ToString();

            var customer = _controller.DeleteCustomer(customerId);

            customer.Should().NotBeNull();
            customer.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void GivenInCorrectIdWhenDeletingCustomerReturnTypeMustBeOfBadRequestObjectResult()
        {
            var customerId = string.Empty;

            var customer = _controller.DeleteCustomer(customerId);

            customer.Should().NotBeNull();
            customer.Should().BeOfType<BadRequestObjectResult>();
        }
    }


}
