using Microsoft.AspNetCore.Mvc;
using We.Sell.Bread.API.Unit.Tests.TestData;
using We.Sell.Bread.Core.DTOs.Product;

namespace We.Sell.Bread.API.Unit.Tests.Tests.Controllers
{
    public class ProductControllerTests : IClassFixture<BaseFixture<ProductController>>
    {
        private readonly BaseFixture<ProductController> _fixture;
        private readonly ProductController _productController;

        public ProductControllerTests(BaseFixture<ProductController> fixture) 
        {
            _fixture = fixture;

            _productController = new ProductController(_fixture.Logger);
        }

        [Fact]
        public void GivenDataExistWhenRetrievingProductReturnTypeMustBeListProductDetailsDtoActionResults()
        {
            var product = _productController.GetAll();

            product.Should().NotBeNull();
            product.Should().BeOfType(typeof(ActionResult<List<ProductDetailsDto>>));
        }

        [Fact]
        public void GivenCorrectIdWhenRetrievingProductReturnTypeMustBeProductDetailsDtoActionResults()
        {
            var product = _productController.GetById(ProductData.ProductIdString);

            product.Should().NotBeNull();
            product.Should().BeOfType(typeof(ActionResult<ProductDetailsDto>));
        }

        [Fact]
        public async Task GivenCorrectDetailsWhenCreatingProductReturnTypeMustBeProductDetailsDtoActionResults()
        {
            var productName = Faker.Name.First();
            var price = Faker.RandomNumber.Next();
            var description = Faker.Name.FullName();
            var stockQuantity = Faker.RandomNumber.Next();

            var product = new NewProductDto(productName, price, description, stockQuantity);

            var response = await _productController.PostAsync(product);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ActionResult<ProductDetailsDto>));
        }
    }
}
