using We.Sell.Bread.Core.DTOs.Product;

namespace We.Sell.Bread.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductDetailsDto>CreateProductAsync(NewProductDto entity);
        ProductDetailsDto GetProductById(string productId);
        IEnumerable<ProductDetailsDto> GetAllProducts();
    }
}
