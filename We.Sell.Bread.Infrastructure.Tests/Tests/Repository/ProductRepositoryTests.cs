using We.Sell.Bread.Core.DTOs.Product;
using We.Sell.Bread.Infrastructure.Helpers;
using We.Sell.Bread.Infrastructure.Repository;

namespace We.Sell.Bread.Infrastructure.Tests.Tests.Repository
{
    public class ProductRepositoryTests
    {
        private ProductRepository _productRepository;
        private string _productFilePath;

        public ProductRepositoryTests()
        {
            _productRepository = new ProductRepository();

            var basePath = FileHelper.GetBasePath();

            _productFilePath = $"{basePath}/We.Sell.Bread.Infrastructure/DataFiles/Customer.json";
        }

        //[Fact]
        //public async void GivenValidIdProductShouldBeRemovedFromProductJson()
        //{
        //    var productName = Faker.Name.First();
        //    var price = Faker.RandomNumber.Next();
        //    var description = Faker.Name.FullName();
        //    var stockQuantity = Faker.RandomNumber.Next();

        //    var productCommand = new ProductCommand(productName, price, description, stockQuantity);

        //    var product = await _productRepository.CreateProductAsync(productCommand);
            
        //    var productId  = product.Id;

        //    await _productRepository.DeleteProductAsync(productId.ToString());

        //    var productJson = JsonHelper.ReadJsonFile(_productFilePath);

        //    var products = JsonHelper.Deserialize<List<ProductDto>>(productJson);

        //    products.FirstOrDefault( prouct => product.Id == productId).Should().BeNull();
        //}
    }
}
