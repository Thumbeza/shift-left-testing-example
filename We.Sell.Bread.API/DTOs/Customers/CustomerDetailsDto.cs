namespace We.Sell.Bread.API.DTOs.Customers;

public class CustomerDetailsDto
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; }
    public string ContactNo { get; set; }
    public string EmailAddress { get; set; }
    public string PhysicalAddress { get; set; }
}
