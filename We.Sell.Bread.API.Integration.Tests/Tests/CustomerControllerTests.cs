using We.Sell.Bread.API.Controllers;
using We.Sell.Bread.Core.DTOs.Customer;

namespace We.Sell.Bread.API.Integration.Tests.Tests
{
    public class CustomerControllerTests : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;
        public CustomerControllerTests(BaseFixture fixture) 
        {
            _fixture = fixture;
        }

        [Fact]
        public void CanCreateCustomer()
        {
            //need to mock api client and adapt this test to make api calls via a client
            var customerController = new CustomerController(_fixture.Logger);

            var customer = new NewCustomerDto
                (
                Faker.Name.FullName(),
                Faker.Phone.Number(),
                Faker.Internet.Email(),
                Faker.Address.City()
                );

            var response = customerController.Post(customer);

            response.Should().NotBeNull();

        }
    }
}
