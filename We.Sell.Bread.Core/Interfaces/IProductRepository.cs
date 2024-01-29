using We.Sell.Bread.Core.DTOs.Product;

namespace We.Sell.Bread.Core.Interfaces
{
    public interface IProductRepository
    {
        ProductDetailsDto GetProductById(string productId);
        IEnumerable<ProductDetailsDto> GetAllProducts();
    }
}
