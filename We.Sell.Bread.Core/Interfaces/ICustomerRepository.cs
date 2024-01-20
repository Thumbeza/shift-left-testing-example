using We.Sell.Bread.Core.DTOs.Customer;

namespace We.Sell.Bread.Core.Interfaces
{
    public interface ICustomerRepository
    {
        CustomerDetailsDto CreateCustomer(NewCustomerDto entity);
        CustomerDetailsDto GetCustomerById(string customerId);
        IEnumerable<CustomerDetailsDto> GetAllCustomers();
        CustomerDetailsDto UpdateCustomer(string customerId, NewCustomerDto entity);
        bool DeleteCustomer(string customerId);
    }
}
