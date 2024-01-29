using We.Sell.Bread.API.Services;
using We.Sell.Bread.Core.DTOs.Product;

namespace We.Sell.Bread.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    private readonly ProductService _productService;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;

        _productService = new ProductService();
    }

    [HttpGet(Name = "PingProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> HealthCheck()
    {
        return Ok("Product Is ALive");
    }

    [HttpPost(Name = "CreateProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDetailsDto>> PostAsync(NewProductDto product)
    {
        _logger.LogInformation($"Attempting to create a neew product: {product.ProductName}");

        var productDetails = await _productService.AddNewProductAsync(product.ProductName, product.Price, product.Description, product.StockQuantity);

        return productDetails == null ? BadRequest("One or more product details were invalid") : productDetails;
    }

    [HttpGet(Name = "GetProductById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ProductDetailsDto> GetById(string productId)
    {

        _logger.LogInformation($"Getting product by Id: {productId}");

        var isValid = Guid.TryParse(productId, out _);

        if (!isValid)
        {
            return BadRequest($"Product Id: '{productId}' is not a valid Guid.");
        }

        var productDetails = _productService.GetProduct(new Guid(productId));

        _logger.LogInformation($"A product with Id: '{productId}' has been retrieved");

        return productDetails == null ? NotFound($"The product with id: {productId} was not found.") : productDetails;
    }

    [HttpGet(Name = "GetAllProducts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<ProductDetailsDto>> GetAll()
    {
        _logger.LogInformation($"Getting all products.");

        var products = _productService.GetAllProducts().ToList();

        _logger.LogInformation("All products have been retrieved");

        return products.Count == 0 ? NotFound($"No products were found.") : products;
    }
}
