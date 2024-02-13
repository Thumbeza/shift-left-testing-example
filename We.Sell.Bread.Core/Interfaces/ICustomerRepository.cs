using We.Sell.Bread.Core.DTOs.Customer;

namespace We.Sell.Bread.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerDto> CreateCustomerAsync(CustomerCommand entity);
        CustomerDto GetCustomerById(string customerId);
        IEnumerable<CustomerDto> GetAllCustomers();
        Task<CustomerDto> UpdateCustomer(CustomerDto entity);
        Task<bool> DeleteCustomerAsync(string customerId);
    }
}
