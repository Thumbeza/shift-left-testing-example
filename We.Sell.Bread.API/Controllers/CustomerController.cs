using We.Sell.Bread.API.Services;
using We.Sell.Bread.Core.DTOs.Customer;

namespace We.Sell.Bread.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;

    private readonly CustomerService _customerService;

    public CustomerController(ILogger<CustomerController> logger)
    {
        _logger = logger;

        _customerService = new CustomerService();
    }

    [HttpGet(Name = "PingCustomer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> HealthCheck()
    {
        return Ok("pong");
    }

    [HttpPost(Name = "CreateCustomer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CustomerDetailsDto> Post(NewCustomerDto customer)
    {
        _logger.LogInformation($"Attempting to create a neew customer: {customer.CustomerName}");

        var customerDetails = _customerService.AddNewCustomer(customer.CustomerName, customer.ContactNo, customer.EmailAddress, customer.PhysicalAddress);

        _logger.LogInformation($"New custormer hase been created: {customerDetails.Id}");

        return customerDetails == null? BadRequest("One or more customer details were invalid") : customerDetails;
    }


    [HttpGet(Name = "GetCustomerById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CustomerDetailsDto> GetById(string customerId)
    {

        _logger.LogInformation($"Getting customer by Id: {customerId}");

        var isValid = Guid.TryParse(customerId, out _);

        if (!isValid)
        {
            return BadRequest($"Customer Id: '{customerId}' is not a valid Guid.");
        }

        var customerDetails = _customerService.GetCustomerDetails(new Guid(customerId));

        _logger.LogInformation($"A customer with Id: '{customerId}' has been retrieved");

        return customerDetails == null ? NotFound($"The custormer with id: {customerId} was not found.") : customerDetails;
    }

    [HttpGet(Name = "GetAllCustomers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<CustomerDetailsDto>> GetAll()
    {
        _logger.LogInformation($"Getting all customers.");

        var customers = _customerService.GetAllCustomers().ToList();

        _logger.LogInformation("All customers have been retrieved");

        return customers.Count == 0 ? NotFound($"No customers were found.") : customers;
    }
}
