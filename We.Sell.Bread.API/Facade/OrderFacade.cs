using We.Sell.Bread.API.Services;
using We.Sell.Bread.Core.DTOs.Order;

namespace We.Sell.Bread.API.Facade
{
    //Order functionality is complext and is dependant on different services (sub-systems)
    //So, to avoid over-complicating the order API, order operations can be extracted using the Facade interface and have the API just call the Facade methods
    public class OrderFacade
    {
        private readonly CustomerService _customer;

        public OrderFacade() 
        {
            _customer = new CustomerService();
        }

        public OrderDetailsDto PlaceOrder(Guid customerId)
        {
            //Get customer details
            var customerDetails = _customer.GetCustomer(customerId);

            //Add items to cart

            //Generate Invoice

            //Make payment

            //Send invoice email

            return new OrderDetailsDto();

        }

        public bool CancelOrder(Guid orderId)
        {
            //Get order details

            //reverse payments

            //send cancellation email

            return true;           
        }
    }
}
