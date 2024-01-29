using We.Sell.Bread.Core.DTOs.Product;

namespace We.Sell.Bread.Core.Interfaces
{
    public interface IProductService
    {
        ProductDetailsDto GetProduct(Guid productId);
        IEnumerable<ProductDetailsDto> GetAllProducts();
    }
}
