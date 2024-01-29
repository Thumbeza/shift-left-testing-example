using We.Sell.Bread.Core.DTOs.Customer;

namespace We.Sell.Bread.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDetailsDto> AddNewCustomerAsync(string customerName, string contactNo, string emailAddress, string physicalAddress);
        CustomerDetailsDto GetCustomer(Guid id);
        Task<bool> DeleteCustomerAsync(Guid id);
        IEnumerable<CustomerDetailsDto> GetAllCustomers();
    }
}
