using Microsoft.AspNetCore.Mvc;
using We.Sell.Bread.Core.DTOs.Product;

namespace We.Sell.Bread.API.Unit.Tests.Tests.Controllers
{
    public class ProductControllerTests : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;
        private readonly ProductController _productController;

        public ProductControllerTests(BaseFixture fixture) 
        {
            _fixture = fixture;

            _productController = new ProductController(_fixture.iLogger);
        }

        [Fact]
        public void GivenDataExistWhenRetrievingProductReturnTypeMustBeListProductDetailsDtoActionResults()
        {
            var product = _productController.GetAll();

            product.Should().NotBeNull();
            product.Should().BeOfType(typeof(ActionResult<List<ProductDetailsDto>>));
        }
    }
}
