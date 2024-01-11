using Microsoft.Extensions.Logging;
using Moq;
using We.Sell.Bread.API.Controllers;
using We.Sell.Bread.API.DTOs.Customers;

namespace We.Sell.Bread.API.Unit.Tests.Tests.Controllers
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

        //[Fact]
        //public void GivenNullModelWhenAddingNewCustomerThrowArgumentNullException()
        //{
        //    var customerController = new CustomerController(_logger);
        //    var emptyCustomer = new CustomerDto { };

        //    var customer = () => customerController.Post(emptyCustomer);

        //    customer.Should().Throw<ArgumentNullException>().WithMessage("Customer details must be added before adding a new customer.");
        //}
    }
}
