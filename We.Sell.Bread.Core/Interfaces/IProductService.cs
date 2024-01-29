using We.Sell.Bread.Core.DTOs.Product;

namespace We.Sell.Bread.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductDetailsDto> AddNewProductAsync(string productName, decimal price, string description, int stockQuantity);
        ProductDetailsDto GetProduct(Guid productId);
        IEnumerable<ProductDetailsDto> GetAllProducts();
    }
}
