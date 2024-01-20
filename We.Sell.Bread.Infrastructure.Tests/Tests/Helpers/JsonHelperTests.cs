using Newtonsoft.Json;
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
            //Need to chnage this to relative path.
            var path = "../We.Sell.Bread.Infrastructure.Tests/TestData/JsonHelper/testingFile.json";
            //C:\Users\kwanele.nzimande\Documents\POC\shift-left-testing-example\We.Sell.Bread.Infrastructure.Tests\TestData\JsonHelper\testingFile.json

            var result = JsonHelper.ReadJsonFile(path);

            result.Should().NotBeNull();
        }

        [Fact]
        public void GivenEmptyStringWhenDeserializingToDataModelThrowArgumentENullxception()
        {
            var emptyString = string.Empty;

            var result = () => JsonHelper.Deserialize<CustomerDetailsDto>(emptyString);

            result.Should().Throw<ArgumentNullException>().WithMessage(" cannot be empty or null");
        }

        [Fact]
        public void GivenIncorrectStringWhenDeserializingToDataModelThrowJsonReaderException()
        {
            var incorrectString = "Testing";

            var result = () => JsonHelper.Deserialize<CustomerDetailsDto>(incorrectString);

            result.Should().Throw<JsonReaderException>();
        }

        [Fact]
        public void GivenCorrectStringObjectWhenDeserializingToDataModelReturnSomeData()
        {
            //Need to chnage this to relative path.
            var path = "C:\\Users\\kwanele.nzimande\\Documents\\POC\\shift-left-testing-example\\We.Sell.Bread.Infrastructure.Tests\\TestData\\JsonHelper\\testingFile.json";
          
            var stringObject = JsonHelper.ReadJsonFile(path);

            var result = JsonHelper.Deserialize<IEnumerable<CustomerDetailsDto>>(stringObject);

            result.Should().NotBeNull();
            result.ToList().Should().HaveCount(2);
        }

    }
}
