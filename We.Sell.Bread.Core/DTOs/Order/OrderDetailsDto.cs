using We.Sell.Bread.Core.DTOs.Customer;

namespace We.Sell.Bread.Core.DTOs.Order
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }
        public CustomerDetailsDto CustomerDetails { get; set; }

    }
}
