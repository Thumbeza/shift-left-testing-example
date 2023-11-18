namespace We.Sell.Bread.API.DTOs.Customers
{
    public class AddCustomer
    {
        public class Request
        {
            public string CustomerName { get; set; }
            public string ContactNo { get; set; }
            public string EmailAddress { get; set; } = string.Empty;
            public string PhysicalAddress { get; set; }
        }

        public class Response
        {
            public Guid Id { get; set; }
            public string CustomerName { get; set; }
        }
    }
}
