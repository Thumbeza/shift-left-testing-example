using We.Sell.Bread.Core.DTOs.Product;

namespace We.Sell.Bread.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> AddNewProductAsync(string productName, decimal price, string description, int stockQuantity);
        ProductDto GetProduct(Guid productId);
        IEnumerable<ProductDto> GetAllProducts();
    }
}
