
using We.Sell.Bread.API.Unit.Tests.TestData;
using We.Sell.Bread.Core.DTOs.Product;

namespace We.Sell.Bread.API.Unit.Tests.Tests.Services
{
    public class ProductServiceTests
    {
        private ProductService _productService;

        public ProductServiceTests()
        {
            _productService = new ProductService();
        }

        [Fact]
        public void GivenDataExistWhenRetrievingProductReturnTypeMustBeListOfProductDetailsDto()
        {
            var product = _productService.GetAllProducts();

            product.Should().NotBeNull();
            product.Should().BeOfType(typeof(List<ProductDto>));
        }

        [Fact]
        public void GivenCorrectIdShouldExistAndReturnTypeObjectProductDetailsDto()
        {
            ProductDto? product = _productService.GetProduct(ProductData.ProductIdGuid);

            product.Should().NotBeNull();
            product.Should().BeOfType(typeof(ProductDto));
        }

        [Fact]
        public void GivenEmptyIdWhenRetrievingProductThrowsFormatException()
        {
            var emptyId = string.Empty;

            var product = () => _productService.GetProduct(new Guid(emptyId));

            product.Should().Throw<FormatException>().WithMessage("Unrecognized Guid format.");
        }

        [Fact]
        public void GivenIncorrectIdWhenRetrievingProductReturnTypeMustBeNull()
        {
            var product = _productService.GetProduct(ProductData.IncorrectProductIdGuid);

            product.Should().BeNull();
        }

        [Fact]
        public async Task GivenCorrectDetailsWhenAddingNewProductShouldReturnTypeProductDetailsDto()
        {
            var productName = Faker.Lorem.GetFirstWord();
            var price = Convert.ToDecimal(Faker.RandomNumber.Next(50));
            var description = Faker.Lorem.Sentence(10);
            var stockQuantity = Faker.RandomNumber.Next(90);

            var product = await _productService.AddNewProductAsync(productName, price, description, stockQuantity);

            product.Should().NotBeNull();
            product.Should().BeOfType(typeof(ProductDto));
            product.ProductName.Should().Be(productName);
            product.Price.Should().Be(price);
            product.Description.Should().Be(description);
            product.StockQuantity.Should().Be(stockQuantity);
        }

        [Fact]
        public async Task GivenEmptyProductNameWhenAddingNewProductThrowFormatException()
        {
            var productName = string.Empty;
            var price = Convert.ToDecimal(Faker.RandomNumber.Next(50));
            var description = Faker.Lorem.Sentence(10);
            var stockQuantity = Faker.RandomNumber.Next(90);

            var product = async () => await _productService.AddNewProductAsync(productName, price, description, stockQuantity);

            await product.Should().ThrowAsync<ArgumentException>().WithMessage(" cannot be empty or null");
        }

        [Fact]
        public async Task GivenEmptyProductDescriptionWhenAddingNewProductThrowFormatException()
        {
            var productName = Faker.Lorem.GetFirstWord();
            var price = Convert.ToDecimal(Faker.RandomNumber.Next(50));
            var description = string.Empty;
            var stockQuantity = Faker.RandomNumber.Next(90);

            var product = async () => await _productService.AddNewProductAsync(productName, price, description, stockQuantity);

            await product.Should().ThrowAsync<ArgumentException>().WithMessage(" cannot be empty or null"); 
        }
    }
}