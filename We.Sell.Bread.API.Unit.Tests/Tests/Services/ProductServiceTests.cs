
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
            product.Should().BeOfType(typeof(List<ProductDetailsDto>));
           
        }

        [Fact]
        public void AddingANewProductShouldReturnAProductAndShouldContainTheGivenDetails()
        {
            string productName = "Ciabatta";
            decimal price = (decimal)35.00;
            string description = "Ciabatta, derived from the Italian word for slipper, is a type of light and airy bread with a slightly chewy crust.";
            int stockQuantity = 15;

            Task<ProductDetailsDto> task = _productService.AddNewProductAsync(productName, price, description, stockQuantity);
            ProductDetailsDto? taskResult = task.Result;

            taskResult.Should().NotBeNull();
            taskResult.Should().BeOfType(typeof(ProductDetailsDto));
            taskResult.ProductName.Should().Contain(productName);
            Assert.Equal(price, taskResult.Price);
            taskResult.Description.Should().Contain(description);
            Assert.Equal(stockQuantity, taskResult.StockQuantity);
        }
    }


}
