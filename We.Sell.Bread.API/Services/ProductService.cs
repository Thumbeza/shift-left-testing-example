using We.Sell.Bread.Core.DTOs.Product;
using We.Sell.Bread.Core.Interfaces;
using We.Sell.Bread.Core.Validations;
using We.Sell.Bread.Infrastructure.Repository;

namespace We.Sell.Bread.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
        }

        public ProductDetailsDto? GetProduct(Guid id)
        {
            Validate.NullOrEmptyArgument(id.ToString());

            var product = _productRepository.GetProductById(id.ToString());

            return product != null ?
                new ProductDetailsDto(id, product.ProductName,product.Price,product.Description,product.StockQuantity) : null;
        }

        public IEnumerable<ProductDetailsDto> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }
    }
}
