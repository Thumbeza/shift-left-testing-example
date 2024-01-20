using Microsoft.Extensions.Logging;
using Moq;

namespace We.Sell.Bread.API.Unit.Tests.Tests
{
    public class BaseFixture
    {
        public readonly ILogger<CustomerController> Logger;

        public BaseFixture() 
        {
            var mock = new Mock<ILogger<CustomerController>>();

            Logger = mock.Object;
        }
    }
}
