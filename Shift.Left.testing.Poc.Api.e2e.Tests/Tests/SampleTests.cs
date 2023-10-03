using FluentAssertions;

namespace Shift.Left.Testing.Poc.Api.e2e.Tests.Tests
{
    public class SampleTests
    {
        [Fact]
        public void Example()
        {
            "Sphe".Should().NotContain("f");
        }
    }
}
