using We.Sell.Bread.Core.DTOs.Customer;

namespace We.Sell.Bread.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerDetailsDto> CreateCustomerAsync(NewCustomerDto entity);
        CustomerDetailsDto GetCustomerById(string customerId);
        IEnumerable<CustomerDetailsDto> GetAllCustomers();
        Task<CustomerDetailsDto> UpdateCustomer(CustomerDetailsDto entity);
        Task<bool> DeleteCustomerAsync(string customerId);
    }
}
