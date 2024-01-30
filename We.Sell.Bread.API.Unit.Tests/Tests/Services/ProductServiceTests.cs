
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
        public void GivenIdShouldExistAndReturnTypeObjectProductDetailsDto()
        {
            ProductDetailsDto? product = _productService.GetProduct(new Guid("411fbad3-925e-4044-9269-f962641f9277"));
            string realName = "Pumpernickel Bread";
            decimal realPrice = (decimal)20.00;
            string realDescription = "A dense, dark bread made from coarsely ground whole rye grains";
            int realStock = 7;

            product.Should().NotBeNull();
            product.Should().BeOfType(typeof(ProductDetailsDto));
            Assert.Equal(realName, product.ProductName);
            Assert.Equal(realPrice, product.Price);
            Assert.Equal(realDescription, product.Description);
            Assert.Equal(realStock, product.StockQuantity);

        }
    }
}
