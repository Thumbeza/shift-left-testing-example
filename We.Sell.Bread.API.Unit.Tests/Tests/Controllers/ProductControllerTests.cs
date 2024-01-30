using Microsoft.AspNetCore.Mvc;
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
    }
}
