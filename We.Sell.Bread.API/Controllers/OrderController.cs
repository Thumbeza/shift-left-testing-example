using We.Sell.Bread.Core.DTOs.Order;

namespace We.Sell.Bread.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OrderController(ILogger<OrderController> logger) : ControllerBase
{
    private readonly ILogger<OrderController> _logger = logger;
    private readonly OrderFacade _orderFacade = new();

    [HttpPost(Name = "CreateOrder")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<OrderDetailsDto> Post(string customerId)
    {

        _logger.LogInformation("Attempting to create a new order");

        if (customerId == null) 
        {
            return BadRequest("A valid customer must be added before placing an order.");
        }

        var order = _orderFacade.PlaceOrder(new Guid(customerId));

        _logger.LogInformation($"New order has been created: {order.Id}");

        return order == null ? BadRequest() : order;
    }
}
