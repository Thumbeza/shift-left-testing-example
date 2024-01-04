using Microsoft.Extensions.Logging;
using Moq;
using We.Sell.Bread.API.Controllers;

namespace We.Sell.Bread.API.Unit.Tests.Tests
{
    public class CustomerControllerTests
    {
        private readonly ILogger<CustomerController> _logger;
        public CustomerControllerTests() 
        {
            var mock = new Mock<ILogger<CustomerController>>();

            _logger = mock.Object;
        }

        [Fact]
        public void GivenEmptyIdWhenRetrievingCustomerThrowArgumentNullException()
        {
            var customerController = new CustomerController(_logger);
            var emptyId = string.Empty;

            var customer = () => customerController.GetById(emptyId);

            customer.Should().Throw<ArgumentNullException>().WithMessage("Customer Id cannot not be null.");
        }
    }
}
