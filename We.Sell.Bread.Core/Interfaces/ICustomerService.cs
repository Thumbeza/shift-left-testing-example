using We.Sell.Bread.Core.DTOs.Customer;

namespace We.Sell.Bread.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> AddNewCustomerAsync(string customerName, string contactNo, string emailAddress, string physicalAddress);
        CustomerDto GetCustomer(Guid id);
        Task<bool> DeleteCustomerAsync(Guid id);
        IEnumerable<CustomerDto> GetAllCustomers();
    }
}
