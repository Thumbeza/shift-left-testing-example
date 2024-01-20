using System.IO;
using We.Sell.Bread.Core.DTOs.Customer;
using We.Sell.Bread.Infrastructure.Helpers;

namespace We.Sell.Bread.Infrastructure.Tests.Tests.Helpers
{
    public class JsonHelperTests
    {
        [Fact]
        public void GivenEmptyPathWhenReadingJsonFilleThrowArgumentENullxception() 
        { 
            var emptyPath = string.Empty;

            var result = () => JsonHelper.ReadJsonFile(emptyPath);

            result.Should().Throw<ArgumentNullException>().WithMessage(" cannot be empty or null");
        }

        [Fact]
        public void GivenInvalidPathWhenReadingJsonFilleThrowFileNotFoundExceptionxception()
        {
            var invalidPath = "Some invallid path";

            var result = () => JsonHelper.ReadJsonFile(invalidPath);

            result.Should().Throw<FileNotFoundException>().WithMessage($"The file could not be found: {invalidPath}");
        }

        [Fact]
        public void GivenValidPathWhenReadingJsonFilleReturnSomeData()
        {
            var path = "C:\\dev\\shift-left-testing-example\\We.Sell.Bread.Infrastructure.Tests\\TestData\\JsonHelper\\testingFile.json";

            var result = JsonHelper.ReadJsonFile(path);

            result.Should().NotBeNull();
        }
    }
}
