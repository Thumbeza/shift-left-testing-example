using We.Sell.Bread.Core.DTOs.Product;
using We.Sell.Bread.Core.Interfaces;
using We.Sell.Bread.Infrastructure.Helpers;

namespace We.Sell.Bread.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private string _basePath;
        private string _ProductFilePath;

        public ProductRepository() 
        {
            _basePath = FileHelper.GetBasePath();

            _ProductFilePath = $"{_basePath}/We.Sell.Bread.Infrastructure/DataFiles/Product.json"; ;
        }

        public IEnumerable<ProductDetailsDto> GetAllProducts()
        {
            var productJson = JsonHelper.ReadJsonFile(_ProductFilePath);

            var products = JsonHelper.Deserialize<IEnumerable<ProductDetailsDto>>(productJson);

            return products;
        }

        public ProductDetailsDto? GetProductById(string productId)
        {
            var productJson = JsonHelper.ReadJsonFile(_ProductFilePath);

            var products = JsonHelper.Deserialize<IEnumerable<ProductDetailsDto>>(productJson);

            var product = products.FirstOrDefault(prod => prod.ProductId.ToString() == productId);

            return product;
        }
    }
}
