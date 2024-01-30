using Microsoft.Extensions.Logging;
using Moq;

namespace We.Sell.Bread.API.Unit.Tests.Tests
{
    public class BaseFixture<T>
    { 
        public readonly ILogger<T> Logger;

        public BaseFixture() 
        {
            var mock = new Mock<ILogger<T>>();

            Logger = mock.Object;
        }
    }
}
