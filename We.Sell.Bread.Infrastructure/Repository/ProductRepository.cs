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

        public async Task<ProductDetailsDto> CreateProductAsync(NewProductDto entity)
        {
            var productJson = JsonHelper.ReadJsonFile(_ProductFilePath);

            var products = JsonHelper.Deserialize<List<ProductDetailsDto>>(productJson);

            var newProduct = new ProductDetailsDto(new Guid(), entity.ProductName, entity.Price, entity.Description, entity.StockQuantity)
            {
                ProductId = Guid.NewGuid(),
                ProductName = entity.ProductName,
                Price = entity.Price,
                Description = entity.Description,
                StockQuantity = entity.StockQuantity,
            };

            products.Add(newProduct);


            await JsonHelper.StreamWriteAsync(products, _ProductFilePath);

            return newProduct;
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
