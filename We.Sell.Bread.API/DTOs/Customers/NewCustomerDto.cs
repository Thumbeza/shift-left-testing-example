﻿namespace We.Sell.Bread.API.DTOs.Customers;

public class NewCustomerDto
{
    public string CustomerName { get; set; }
    public string ContactNo { get; set; }
    public string EmailAddress { get; set; }
    public string PhysicalAddress { get; set; }
}