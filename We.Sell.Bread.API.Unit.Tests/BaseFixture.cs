using Microsoft.Extensions.Logging;
using Moq;

namespace We.Sell.Bread.API.Unit.Tests.Tests
{
    public class BaseFixture
    {
        public readonly ILogger<CustomerController> Logger;
        public readonly ILogger<ProductController> iLogger;

        public BaseFixture() 
        {
            var mock = new Mock<ILogger<CustomerController>>();
            var imock = new Mock<ILogger<ProductController>>();

            Logger = mock.Object;
            iLogger = imock.Object;
        }
    }
}
