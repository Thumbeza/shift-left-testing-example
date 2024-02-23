using We.Sell.Bread.Core.DTOs.Product;
using We.Sell.Bread.Core.Interfaces;
using We.Sell.Bread.Infrastructure.Helpers;

namespace We.Sell.Bread.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _basePath;
        private readonly string _ProductFilePath;

        public ProductRepository()
        {
            _basePath = FileHelper.GetBasePath();

            _ProductFilePath = $"{_basePath}/We.Sell.Bread.Infrastructure/DataFiles/Product.json"; ;
        }

        public async Task<ProductDto> CreateProductAsync(ProductCommand entity)
        {
            var productJson = JsonHelper.ReadJsonFile(_ProductFilePath);

            var products = JsonHelper.Deserialize<List<ProductDto>>(productJson);

            var newProduct = new ProductDto(new Guid(), entity.ProductName, entity.Price, entity.Description, entity.StockQuantity)
            {
                Id = Guid.NewGuid(),
                ProductName = entity.ProductName,
                Price = entity.Price,
                Description = entity.Description,
                StockQuantity = entity.StockQuantity,
            };

            products.Add(newProduct);


            await JsonHelper.StreamWriteAsync(products, _ProductFilePath);

            return newProduct;
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            var productJson = JsonHelper.ReadJsonFile(_ProductFilePath);

            var products = JsonHelper.Deserialize<IEnumerable<ProductDto>>(productJson);

            return products;
        }

        public ProductDto? GetProductById(string productId)
        {
            var productJson = JsonHelper.ReadJsonFile(_ProductFilePath);

            var products = JsonHelper.Deserialize<IEnumerable<ProductDto>>(productJson);

            var product = products.FirstOrDefault(prod => prod.Id.ToString() == productId);

            return product;
        }

        public async Task<ProductDto> UpdateProduct(ProductDto entity)
        {
            var productJsonString = JsonHelper.ReadJsonFile(_ProductFilePath);

            var existingproductsList = JsonHelper.Deserialize<IList<ProductDto>>(productJsonString);

            var productDtoToFind = existingproductsList.FirstOrDefault(x => x.Id.ToString() == entity.Id.ToString());

            existingproductsList.Remove(productDtoToFind);
            existingproductsList.Add(entity);

            await JsonHelper.StreamWriteAsync(existingproductsList, _ProductFilePath);

            return entity;
        }
    }
}
