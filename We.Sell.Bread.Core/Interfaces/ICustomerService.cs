using We.Sell.Bread.Core.DTOs.Customer;

namespace We.Sell.Bread.Core.Interfaces
{
    public interface ICustomerService
    {
        CustomerDetailsDto AddNewCustomer(string customerName, string contactNo, string emailAddress, string physicalAddress);
        CustomerDetailsDto GetCustomerDetails(Guid id);
        bool DeleteCustomerDetails(Guid id);
        IEnumerable<CustomerDetailsDto> GetAllCustomers();
    }
}
