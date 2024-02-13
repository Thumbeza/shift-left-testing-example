using We.Sell.Bread.API.Services;
using We.Sell.Bread.Core.DTOs.Customer;

namespace We.Sell.Bread.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CustomerController(ILogger<CustomerController> logger) : ControllerBase
{
    private readonly ILogger<CustomerController> _logger = logger;

    private readonly CustomerService _customerService = new();

    [HttpGet(Name = "PingCustomer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> HealthCheck()
    {
        return Ok("pong");
    }

    [HttpPost(Name = "CreateCustomer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerDto>> Post(CustomerCommand customer)
    {
        _logger.LogInformation($"Attempting to create a new customer: {customer.CustomerName}");

         var customerDetails = await _customerService.AddNewCustomerAsync(customer.CustomerName, customer.ContactNo, customer.EmailAddress, customer.PhysicalAddress);

         return customerDetails == null? BadRequest("One or more customer details were invalid") : customerDetails;
    }


    [HttpGet, Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CustomerDto> Get(string id)
    {

        _logger.LogInformation($"Getting customer by Id: {id}");

        var isValid = Guid.TryParse(id, out _);

        if (!isValid)
        {
            return BadRequest($"Customer Id: '{id}' is not a valid Guid.");
        }

        var customerDetails = _customerService.GetCustomer(new Guid(id));

        _logger.LogInformation($"A customer with Id: '{id}' has been retrieved");

        return customerDetails == null ? NotFound($"The customer with id: {id} was not found.") : customerDetails;
    }

    [HttpGet, Route("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<List<CustomerDto>> Get()
    {
        _logger.LogInformation($"Getting all customers.");

        var customers = _customerService.GetAllCustomers().ToList();

        _logger.LogInformation("All customers have been retrieved");

        return customers.Count == 0 ? NoContent() : customers;
    }

    [HttpDelete, Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(string id) 
    {
        _logger.LogInformation($"Deleting customer details by Id: {id}");

        var isValid = Guid.TryParse(id, out _);

        if (!isValid)
        {
            return BadRequest($"Customer Id: '{id}' is not a valid Guid.");
        }

        var IsDeletionSuccessful = await _customerService.DeleteCustomerAsync(new Guid(id));

        return IsDeletionSuccessful == false ? BadRequest() : NoContent();
    }

    [HttpPut, Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerDto>> Put(string id, CustomerCommand newCustomer)
    {
        _logger.LogInformation($"Updating the following customer's details: {newCustomer.CustomerName}");

        var isValid = Guid.TryParse(id, out _);

        if (!isValid)
        {
            return BadRequest($"Customer Id: '{id}' is not a valid Guid.");
        }

        var customer = _customerService.GetCustomer(Guid.Parse(id));

        if (customer != null)
        {
            var customerDetails = await _customerService.UpdateCustomerDetailsAsync(id, newCustomer);
            
            if (customerDetails != null)
            {
                _logger.LogInformation($"Customer: {customerDetails.Id} details have been updated.");
            }

            return customerDetails == null ? BadRequest("One or more customer details were invalid") : customerDetails;
        }

        return BadRequest($"One or more customer details were invalid");
    }
}
