using We.Sell.Bread.Core.DTOs.Order;

namespace We.Sell.Bread.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly OrderFacade _orderFacade;

    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;
        _orderFacade = new OrderFacade();
    }

    [HttpPost(Name = "CreateOrder")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<OrderDetailsDto> Post(string custormerId)
    {

        _logger.LogInformation("Attempting to create a new order");

        if (custormerId == null) 
        {
            return BadRequest("A valid customer must be added before placing an order.");
        }

        var order = _orderFacade.PlaceOrder(new Guid(custormerId));

        _logger.LogInformation($"New order hase been created: {order.Id}");

        return order == null ? BadRequest() : order;
    }
}
